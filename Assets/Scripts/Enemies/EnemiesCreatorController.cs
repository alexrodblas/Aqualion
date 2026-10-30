using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCreatorController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    
    private int MAX_ENEMIES_NUM;
    private float spawnEnemiesCD;

    void Start()
    {
        spawnEnemiesCD = GameController.instance.getConfig().enemiesSpawnCD;
        MAX_ENEMIES_NUM = GameController.instance.getConfig().MAX_ENEMIES_NUM;
    }

    void Update()
    {
        spawnEnemiesCD -= Time.deltaTime;
        if (spawnEnemiesCD <= 0)
        {
            spawnEnemiesCD = (GameController.instance.getConfig().enemiesSpawnCD * 60 / GameController.instance.GetTime());
            if(spawnEnemiesCD > 7.5f)
            {
                spawnEnemiesCD = 7.5f;
            }

            if(transform.childCount < MAX_ENEMIES_NUM )
            {
                SpawnRandomEnemy();
            }
        }
    }

    /// <summary>
    /// Crea un enemigo aleatorio
    /// </summary>
    void SpawnRandomEnemy()
    {
        int indRandomEnemy = Random.Range(0, enemies.Length);
        GameObject newEnemy = Instantiate(enemies[indRandomEnemy], Vector3.zero, Quaternion.identity,transform);
        SetRandomPosition(newEnemy);

    }


    /// <summary>
    /// Determina una posicion aleatoria para el enemigo creado en un limite de +- 20 del player
    /// </summary>
    /// <param name="newEnemy"></param>
    private void SetRandomPosition(GameObject newEnemy)
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        //float randomXPosition = Random.Range(playerPosition.x - 20, playerPosition.x + 20);
        //float randomYPosition = Random.Range(playerPosition.y - 20, playerPosition.y + 20);
        float randomXPosition = Random.Range(-20, 21);
        float randomYPosition = Random.Range(-32, 32);

        newEnemy.transform.position = new Vector3(randomXPosition, randomYPosition,0);
    }
}
