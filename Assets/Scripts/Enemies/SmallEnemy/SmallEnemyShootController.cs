using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SmallEnemyShootController : MonoBehaviour
{
    [SerializeField] EventReference shootSound;
    [SerializeField] GameObject projectil;
    private Transform spawnPoint;
    private float attackCooldown;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.GetChild(0).transform;
        timer = attackCooldown = GameController.instance.getConfig().enemyAttackCD[0];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = attackCooldown;
            Shoot();
        }
    }
    private void Shoot()
    {
        AudioController.instance.playOneShoot(shootSound);
        Instantiate(projectil,spawnPoint.position ,Quaternion.identity);
    }
}
