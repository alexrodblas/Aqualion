using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] int enemyNum;
    
    GameObject target;
    Vector2 dir;
    private Rigidbody2D rb;
    private float speed;
    private bool triggerExited;

    private int damage;

    private const float startChasingCD = .75f;
    private float currentChasingCD;

    private float defaultMeleeAttackCD;
    private float currentMeleeAttackCD;


    private void OnEnable()
    {
        triggerExited = true;
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        currentChasingCD = startChasingCD;
        dir = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();


        damage = GameController.instance.getConfig().enemiesDamage[enemyNum];
        speed = GameController.instance.getConfig().enemiesSpeed[enemyNum];

        currentMeleeAttackCD = defaultMeleeAttackCD = GameController.instance.getConfig().enemyAttackCD[1];
    }

    private void Update()
    {
        UpdateStartChasingCD();
        UpdateMeleeAtackCD();
    }

    private void UpdateMeleeAtackCD()
    {
        if (currentMeleeAttackCD > 0)
        {
            currentMeleeAttackCD -= Time.deltaTime;
        }
    }

    private void UpdateStartChasingCD()
    {
        if (triggerExited & currentChasingCD > 0)
        {
            currentChasingCD -= Time.deltaTime;
        }
        else if (currentChasingCD <= 0)
        {
            triggerExited = false;
            speed = GameController.instance.getConfig().enemiesSpeed[enemyNum];
            currentChasingCD = startChasingCD;
        }
    }

    private void FixedUpdate()
    {
        CalcDirection();
        rb.velocity = dir * speed;
    }

    /// <summary>
    /// Calcula la direccion hacia donde tiene que mirar, al player
    /// </summary>
    private void CalcDirection()
    {
        Vector2 playerPosition = new Vector2 (target.transform.position.x, target.transform.position.y);
        Vector2 enemyPosition = new Vector2 (rb.transform.position.x, rb.transform.position.y);

        dir = (playerPosition - enemyPosition).normalized;
        transform.right = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision == target.GetComponent<Collider2D>())
        {
            currentChasingCD = startChasingCD;
            triggerExited = false;
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target != null)
        {
            if (collision == target.GetComponent<Collider2D>())
            {
                if (!triggerExited)
                {
                    triggerExited = true;
                }
            }
        }
    }

    /// <summary>
    /// Funcion para atacar al player cuerpo a cuerpo cuando se pueda
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target != null)
        {
            if (target.gameObject.GetComponent<Collider2D>() == collision.collider)
            {
                if(currentMeleeAttackCD <= 0)
                {
                    currentMeleeAttackCD = defaultMeleeAttackCD;
                    target.gameObject.GetComponent<PlayerController>().ReciveDamage(damage);
                }
            }
        }
    }

    /// <summary>
    /// Devuelve la direccion hacia donde esta apuntando
    /// </summary>
    /// <returns> La direccion </returns>
    public Vector2 getDir()
    {
        return dir;
    }
    public int getEnemyNum()
    {
        return enemyNum;
    }
}
