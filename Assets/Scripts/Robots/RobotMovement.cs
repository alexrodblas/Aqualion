using System.Collections;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private Vector3 PosicionInicial;
    private float distanciaRecorrida;
    private Rigidbody2D Rb;
    private Animator animator;
    private float lastVelocity;


    [SerializeField] float velocity;
    [SerializeField] bool horizontal;
    [SerializeField] float distancia;
    private float tiempoEnMovimiento, tiempoEnMovimientoActual;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        PosicionInicial = transform.position;
        lastVelocity = velocity;

        tiempoEnMovimientoActual = 0;
        tiempoEnMovimiento = distancia / velocity;

        SetVelocidad();
    }

    private void FixedUpdate()
    {
        Move();
    }


    //Se le actualiza la velocidad nueva
    public void SetVelocidad()
    {
        if (horizontal)
        {
            Rb.velocity = new Vector3(velocity, 0, 0);
        }
        else
        {
            Rb.velocity = new Vector3(0, velocity, 0);
        }
    }

    public void StopMovement()
    {
        velocity = 0;
        animator.enabled = false;
        Rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void ResumeMovement()
    {
        animator.enabled = true;
        velocity = lastVelocity;
        if(velocity > 0)
        {
            StartCoroutine(RotarHaciaGrados(90));
        }
        else
        {
            StartCoroutine(RotarHaciaGrados(-90));
        }
        Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void Move()
    {
        //Calculo la distancia recorrida desde el punto Inicial
        //distanciaRecorrida = (transform.position - PosicionInicial).magnitude;

        //velocity = lastVelocity;

        //Si ha recorrido la misma distancia o mas de la establecida se le invierte la velocidad
        //if (distanciaRecorrida >= distancia)
        //{
        //    velocity *= -1;
        //    lastVelocity = velocity;
        //    StartCoroutine(RotarRobot());
        //}
        if (tiempoEnMovimientoActual >= tiempoEnMovimiento && Rb.velocity != Vector2.zero)
        {
            tiempoEnMovimientoActual = 0;
            velocity *= -1;
            lastVelocity = velocity;
            StartCoroutine(RotarRobot());
        }
        else if (Rb.velocity != Vector2.zero)
        {
            tiempoEnMovimientoActual += Time.deltaTime;
        }
        SetVelocidad();
    }

    IEnumerator RotarRobot()
    {
        for(int i = 0; i < 18; i++)
        {
            transform.Rotate(Vector3.forward, 10);
            yield return new WaitForSeconds(.2f / 18f);
        }
    }

    IEnumerator RotarHaciaGrados(int grados)
    {
        yield return new WaitForSeconds(0f);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,grados));
    }
}
