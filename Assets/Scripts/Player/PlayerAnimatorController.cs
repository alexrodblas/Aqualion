using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            SetAnimation();
            RotateToMouse();
        }
    }

    private void RotateToMouse()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition  = Camera.main.ScreenToWorldPoint(screenPosition);

        //Calcula el vector de la posicion del raton desde el player normalizado
        worldPosition = (new Vector3(worldPosition.x, worldPosition.y, 0) - transform.position).normalized;

        float angle = AngleBetweenTwoPoints(transform.right.normalized, worldPosition);

        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void SetAnimation()
    {
        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
