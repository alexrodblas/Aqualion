using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] int weapon;
    private int damage;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Start()
    {
        damage = GameController.instance.getConfig().weaponsDamage[weapon];
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            collision.gameObject.GetComponent<EnemyHealthController>().ReciveDamage(damage);
        }
    }
}
