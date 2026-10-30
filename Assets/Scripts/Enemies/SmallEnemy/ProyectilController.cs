using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    GameObject player;
    private float speed;
    private Vector2 dir;

    private int damage;
    [SerializeField] private float lifetime = 5;

    private void Awake()
    {

        Destroy(gameObject,lifetime);
        player = GameObject.FindGameObjectWithTag("Player");
        damage = GameController.instance.getConfig().enemiesDamage[0];
        speed = GameController.instance.getConfig().proyectilSpeed;
    }
    private void OnEnable()
    {
        dir = (player.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3 (dir.x * speed*Time.deltaTime, dir.y * speed*Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReciveDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
