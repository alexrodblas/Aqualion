using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] EventReference dashSound;
    [SerializeField] GameObject dashButtonImg;


    private Rigidbody2D rb;
    private float playerSpeed;
    private float currentPlayerSpeed;

    private bool dashing;
    private float dashCD;
    private float currentDashCD;

    private float dashDuration;
    private float currentDashDuration;

    private float dashImpulse;

    private Vector2 dir;

    PlayerInputs playerInputs;

    void Start()
    {
        playerInputs = PlayerController.instance.playerInputs;
        dashButtonImg.SetActive(false);
        dashing = false;

        dashImpulse = GameController.instance.getConfig().dashImpulse;
        currentDashCD = .5f;
        dashCD = GameController.instance.getConfig().dashCD;
        currentDashDuration = dashDuration = GameController.instance.getConfig().dashDuration;
        currentPlayerSpeed = playerSpeed = GameController.instance.getConfig().playerSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Dash();
        dir = playerInputs.Movement.Move.ReadValue<Vector2>();
    }

    private void Dash()
    {
        //Si aun no se puede activar al dash baja el cooldown
        if(currentDashCD > 0)
        {
            currentDashCD -= Time.deltaTime;
        }
        //Si pulsa la Shift se activa el dash
        else if(playerInputs.Movement.Dash.WasPressedThisFrame())
        {
            dashButtonImg.GetComponent<UISpriteAnimation>().Func_PlayUIAnim();
            dashButtonImg.SetActive(false);

            AudioController.instance.playOneShoot(dashSound);
            dashing = true;
            currentDashDuration = dashDuration;
            currentDashCD = dashCD;
            //Si esta en la escena de prueba de controles que el cd del dash sea mas bajo.
            if(SceneManager.GetActiveScene().name == "ControlesScene")
            {
                currentDashCD = 2f;
            }
            currentPlayerSpeed += dashImpulse;
        }
        else if(currentDashCD <= 0)
        {
            if(dashButtonImg.activeInHierarchy == false)
            {
                dashButtonImg.SetActive(true);

                dashButtonImg.GetComponent<UISpriteAnimation>().Func_PlayUIAnim();
            }
        }
        
        //Si el dash se ha acabado se quita la velocidad
        if (currentDashDuration <= 0)
        {
            currentPlayerSpeed -= dashImpulse;
            dashing = false;
            currentDashDuration = dashDuration;
        }
        //Si esta dasheando baja el tiempo restante del dash
        else if(dashing)
        {
            currentDashDuration -= Time.deltaTime;
        }


    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = dir * currentPlayerSpeed;
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }
}
