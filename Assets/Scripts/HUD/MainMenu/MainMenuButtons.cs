using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartDefaultGame()
    {
        GameController.instance.cambiarRatonAMira(true);
        GameController.instance.endless = false;
        SceneManager.LoadScene("PreGameScene");
    }
    public void StartEndlessGame()
    {
        GameController.instance.cambiarRatonAMira(true);
        GameController.instance.endless = true;
        GameController.instance.setIsGameStarted(true);
        SceneManager.LoadScene("GameScene");
    }

    public void StartTrainingMap()
    {
        GameController.instance.cambiarRatonAMira(true);
        PlayerPrefs.SetString("NextScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ControlesScene");
    }


    public void Quit()
    {
        Application.Quit();
    }
}
