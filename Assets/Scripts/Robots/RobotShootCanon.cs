using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RobotShootCanon : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] EventReference shootSound;
    [SerializeField] EventReference sonidoCompra;
    [SerializeField] EventReference sonidoCompraFallida;
    [SerializeField] GameObject avisoJugador;


    int precio;
    float tiempoActivo, tiempoActivoActual;
    float dispararCD, dispararActualCD;
    bool disparando, playerDentro;
    float velocidadBala;
    List<GameObject> enemigosDentro;

    RobotMovement robotMovement;
    ParticleSystem particleSystem;
    GameObject enemigoActual;
    GameObject canon;
    private void Awake()
    {
        avisoJugador.SetActive(false);
        robotMovement = GetComponent<RobotMovement>();
        enemigosDentro = new List<GameObject>();
        dispararCD = dispararActualCD = .5f;
        tiempoActivo = 20f;
        tiempoActivoActual = 0f;
        canon = transform.GetChild(0).gameObject;
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        velocidadBala = GameController.instance.getConfig().proyectilSpeed;
        if (SceneManager.GetActiveScene().name == "ControlesScene")
            precio = 0;
        else
            precio = GameController.instance.getConfig().precioRobots;
    }

    private void Update()
    {
        MostrarAviso();
        if (enemigoActual == null && enemigosDentro.Count > 0)
        {
            GetActualEnemy();
        }
        //Mira al enemigo que ha encontrado primero
        if (enemigoActual != null && tiempoActivoActual > 0f)
        { 
            Vector3 enemyPosition = (new Vector3(enemigoActual.transform.position.x, enemigoActual.transform.position.y,0) - transform.position).normalized;

            float angle = AngleBetweenTwoPoints(transform.right.normalized, enemyPosition*-1);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        //Reduce el tiempo activo cuando se pueda
        if(tiempoActivoActual > 0)
        {
            tiempoActivoActual -= Time.deltaTime;
            CalcularDisparo();
        }
        //Cuando el tiempo se ha acabado y sigue disparando
        else if(disparando)
        {
            robotMovement.ResumeMovement();
            disparando = false;
        }


        if (!disparando && playerDentro && PlayerController.instance.playerInputs.Robot.Interactuar.WasPressedThisFrame())
        { 
            ComprarRobot();
        }
    }

    private void MostrarAviso()
    {
        if (avisoJugador != null)
        {
            if (avisoJugador.activeInHierarchy == false && playerDentro  && !disparando && PlayerController.instance.getCoins() >= precio)
            {
                avisoJugador.gameObject.SetActive(true);
                avisoJugador.gameObject.GetComponent<UISpriteAnimation>().Func_PlayUIAnim();
            }
            else if (!playerDentro || PlayerController.instance.getCoins() < precio || disparando)
            {
                avisoJugador.gameObject.SetActive(false);
                avisoJugador.gameObject.GetComponent<UISpriteAnimation>().Func_StopUIAnim();
            }
        }
        
    }

    private void GetActualEnemy()
    {
        if(enemigoActual == null)
        {
            if (enemigosDentro[0] != null)
            {
                enemigoActual = enemigosDentro[0];
            }
        }
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void ComprarRobot()
    {
        if(PlayerController.instance.getCoins() >= precio)
        {
            AudioController.instance.playOneShoot(sonidoCompra);
            PlayerController.instance.removeCoins(precio);
            robotMovement.StopMovement();
            disparando = true;
            tiempoActivoActual = tiempoActivo;
        }
        else
        {
            AudioController.instance.playOneShoot(sonidoCompraFallida);
        }
    }

    private void CalcularDisparo()
    {
        if (enemigoActual == null)
            return;

        if(disparando)
        {
            if (dispararActualCD > 0)
            {
                dispararActualCD -= Time.deltaTime;
            }
            else
            {
                dispararActualCD = dispararCD;
                Disparar();
            }
        }

    }

    private void Disparar()
    {
        
        AudioController.instance.playOneShoot(shootSound);
        particleSystem.Play();

        //Instanciamos la bala
        GameObject balaInst = Instantiate(bala, canon.transform.GetChild(0).transform.position, transform.rotation);

        //Coge la posicion del enemigo al que esta apuntando actualmente        
        Vector3 aimingPosition = (enemigoActual.transform.position-transform.position);

        balaInst.GetComponent<Rigidbody2D>().velocity = aimingPosition.normalized * velocidadBala;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (enemigoActual == null)
            {
                enemigoActual = collision.gameObject;
            }
            enemigosDentro.Add(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDentro = true;
            
        }
        else if (collision.gameObject.tag == "Enemigo")
        {
            if (enemigoActual == null)
            {
                enemigoActual = collision.gameObject; 
            }
            enemigosDentro.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerDentro = false;
        }
        if(collision.gameObject.tag == "Enemigo")
        {
            enemigosDentro.Remove(collision.gameObject);
        }
    }

}
