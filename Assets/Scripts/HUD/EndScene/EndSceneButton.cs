using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButton : MonoBehaviour
{
    [SerializeField] EventReference eventReference;

    public void GoToMainMenu()
    {
        AudioController.instance.playOneShoot(eventReference);
        SceneManager.LoadScene("MainMenuScene");
    }
}
