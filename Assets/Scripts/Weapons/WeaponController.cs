using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] EventReference shootSound;
    [SerializeField] public int weaponNum;
    [SerializeField] public int weaponValue;
    [SerializeField] GameObject proyectil;

    PlayerInputs playerInputs;

    private float velocidadProyectil;
    private bool shooting;
    private float fireCD;
    private float currentFireCD;

    ParticleSystem particleSystem;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        velocidadProyectil = 40; 
        playerInputs = PlayerController.instance.playerInputs;
        currentFireCD = 0;
        fireCD = GameController.instance.getConfig().weaponsFireCD[weaponNum];
    }
    void Update()
    {
        shooting = playerInputs.Shooting.Shoot.ReadValue<float>() != 0;
        Shoot();
    }

    private void Shoot()
    {
        //Si esta pulsando el boton y el cooldown bajado dispara
        if (Time.timeScale != 0 && shooting & currentFireCD <= 0)
        {
            AudioController.instance.playOneShoot(shootSound);
            particleSystem.Play();

            //Instanciamos la bala
            GameObject bala = Instantiate(proyectil, transform.GetChild(1).transform.position, transform.rotation);

            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            //Calcula el vector de la posicion del raton desde el transform normalizado
            worldPosition = (new Vector3(worldPosition.x, worldPosition.y, 0) - transform.position).normalized;

            bala.GetComponent<Rigidbody2D>().velocity = worldPosition.normalized * velocidadProyectil;

            currentFireCD = fireCD;
        }
        //Si el cooldown no ha llegado baja siempre
        else if(currentFireCD > 0) 
        {
            currentFireCD -= Time.deltaTime;
        }
    }

    public int getWeaponValue()
    {
        return weaponValue;
    }
}
