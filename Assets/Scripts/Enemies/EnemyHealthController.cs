using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] EnemiesCreatorController enemiesCreator;

    private int multiplicadorDeMonedas;
    private int enemyNum;
    private int health;

    // Start is called before the first frame update
    void Awake()
    {
        multiplicadorDeMonedas = GameController.instance.getConfig().multiplicadorMonedas;
        enemyNum = GetComponent<EnemyMovement>().getEnemyNum();
        health = GameController.instance.getConfig().enemiesHealth[enemyNum];
    }

    public bool ReciveDamage(int dmg)
    {
        if (dmg >= health)
        {
            //Sonido
            GenerarMonedas();
            Destroy(gameObject);
            return false;
        }
        else
        {
            StartCoroutine(ParpadearEnRojo());
            health -= dmg;
            return true;
        }
    }

    IEnumerator ParpadearEnRojo()
    {
        SpriteRenderer spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
    }

    private void GenerarMonedas()
    {
        //Genero el numero de monedas que soltara el enemigo de manera random
        //Entre el numero del enemigo y el numero*multiplicador, los dos incluidos.
        int numMonedas = Random.Range(enemyNum+1,multiplicadorDeMonedas*(enemyNum+1)+1);

        for(int i = 0; i < numMonedas; i++)
        {
            GameObject newCoin = Instantiate(coin,transform.position,Quaternion.identity);

            //Le doy una velocidad con una direccion random al rb de la moneda
            //Y una rotacion en Y tambien aleatoria
            float zRotation = Random.Range(-180f, 180f);
            Vector2 dirCoin = new Vector2(Random.Range(-100,101),Random.Range(-100,101));
            
            newCoin.GetComponent<Rigidbody2D>().velocity = dirCoin.normalized * 2;
            newCoin.transform.rotation = Quaternion.Euler(0,0,zRotation);
        }
    }
}
