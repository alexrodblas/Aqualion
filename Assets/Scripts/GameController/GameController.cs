using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField] Texture2D mouseTexture;

    [Header ("Sonidos")]
    [SerializeField] EventReference loseSound;
    [SerializeField] EventReference winSound;

    [SerializeField] private GameConfig config;
    public static GameController instance = null;

    [NonSerialized] public bool endless;
    private bool isGameStarted;
    public float time;

    

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        isGameStarted = false;
    }

    private void Update()
    {
        if (isGameStarted)
        {
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        if(time < getConfig().timeToSurvive | endless)
        {
            time += Time.deltaTime;
        }
        else
        {
            Win();
        }
    }

    private void Win()
    {
        AudioController.instance.playOneShoot(winSound);
        isGameStarted = false;
        time = 0;
        cambiarRatonAMira(false);
        SceneManager.LoadScene("WinScene");
    }
    public void Defeated()
    {
        AudioController.instance.playOneShoot(loseSound);
        isGameStarted = false;
        time = 0;
        cambiarRatonAMira(false);
        SceneManager.LoadScene("DefeatScene");
    }

    public GameConfig getConfig()
    {
        return config;
    }


    public void setIsGameStarted(bool isGameStarted)
    {
        this.isGameStarted = isGameStarted;
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public void GoToMainMenu()
    {
        cambiarRatonAMira(false);
        SceneManager.LoadScene("MainMenuScene");
    }

    public float GetTime() { return time; }

    public void cambiarRatonAMira(bool cambiarAMira)
    {
        if (cambiarAMira)
        {
            Cursor.SetCursor(mouseTexture, new Vector2(mouseTexture.width / 2, mouseTexture.height / 2), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
