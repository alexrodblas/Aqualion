using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    [Header ("Recursos")]
    [SerializeField] public GameObject[] weapons;
    [SerializeField] EventReference hurtSound;
    [SerializeField] EventReference errorCompra;
    
    [Header("HUD")]
    [SerializeField] CoinsTextController coinsTextController;
    [SerializeField] HealthTextController healthTextController;

    public PlayerInputs playerInputs;

    private GameObject weaponObject;
    private int coins, weapon, health;
    private int MAX_HEALTH;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            playerInputs = new();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        coins = 0;
        weapon = 0;
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void Start()
    {
        MAX_HEALTH = health = GameController.instance.getConfig().playerHealth;
        weaponObject = Instantiate(weapons[weapon], transform.GetChild(0).GetChild(0));
    }

    public void addCoin()
    {
        coins +=1;
        coinsTextController.UpdateCoins(coins);
    }
    public void removeCoins(int _coins)
    {
        coins -= _coins;
        coinsTextController.UpdateCoins(coins);
    }

    /// <summary>
    /// Funcion para poner en todos los botones de armas de la tienda
    /// </summary>
    /// <param name="weapon"></param>
    public void SetWeapon(int weapon)
    {
        this.weapon = weapon;
        ActualizarSpriteArma();
    }

    public int getWeapon()
    {
        return weapon;
    }


    public void ReciveDamage(int damage)
    {
        AudioController.instance.playOneShoot(hurtSound);

        if (this.health <= damage)
        {
            this.health = 0;
            GameController.instance.Defeated();
        }
        else
        {
            this.health -= damage;
        }
        healthTextController.UpdateHealth(health);
    }

    public int getCoins()
    {
        return coins;
    }

    public void Heal()
    {

        if (coins >= 10)
        {
            if (health < MAX_HEALTH)
            {
                health++;
                coins -= 10;
                coinsTextController.UpdateCoins(coins);
                healthTextController.UpdateHealth(health);
            }
            else
            {
                AudioController.instance.playOneShoot(errorCompra);
            }
        }
        else
        {
            AudioController.instance.playOneShoot(errorCompra);
        }
    }

    private void ActualizarSpriteArma()
    {
        Destroy(weaponObject);
        weaponObject = Instantiate(weapons[weapon], transform.GetChild(0).GetChild(0));
    }
}