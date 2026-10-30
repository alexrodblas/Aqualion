using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerEnemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            if (rb.velocity.magnitude == 0 && animator.GetBool("InMovement") == true)
            {
                animator.SetBool("InMovement", false);
            }
            else if (rb.velocity.magnitude != 0 && animator.GetBool("InMovement") == false)
            {
                animator.SetBool("InMovement", true);
            }
        }
    }
}
