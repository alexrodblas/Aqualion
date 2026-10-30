using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ParameterScript", menuName = "Config/Parameters")]
public class GameConfig : ScriptableObject
{
    [Header ("GAME SETTINGS")]
    public float timeToSurvive;

    [Header("PLAYER")]
    public float dashDuration;
    public float dashCD;
    public float dashImpulse;
    public int playerHealth;
    public float playerSpeed;

    [Header("WEAPONS")]
    public float[] weaponsFireCD;
    public int[] weaponsDamage;
    public int[] weaponsValue;

    [Header("ENEMIGOS")]
    public int multiplicadorMonedas;
    public int[] enemiesHealth;
    public float[] enemiesSpeed;
    public float enemiesSpawnCD;

    public int MAX_ENEMIES_NUM;

    [Header("ATTACKS")]
    public int[] enemiesDamage;
    public float[] enemyAttackCD;
    public float proyectilSpeed;

    [Header("ROBOTS")]
    public int precioRobots;
}
