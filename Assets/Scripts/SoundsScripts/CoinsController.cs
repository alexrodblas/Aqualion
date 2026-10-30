using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    [SerializeField] EventReference coinSound;

    private float timeMoving;

    private void Awake()
    {
        timeMoving = 2;
    }

    private void Update()
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime*100);
        
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, Time.deltaTime*100);
        if (timeMoving > 0)
        {
            timeMoving -= Time.deltaTime;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>())
        {
            Destroy(gameObject);
            AudioController.instance.playOneShoot(coinSound);
            PlayerController.instance.addCoin();
        }
        else if(collision.gameObject.tag == "Muro")
        {
            timeMoving = 0;
        }
    }

}
