using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] OpcionesMenu menuVolumen;
    private bool abierto;
    private PlayerInputs playerInputs;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        playerInputs = PlayerController.instance.playerInputs;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }
    void Update()
    {
        if(!abierto && playerInputs.MenuNav.PauseMenu.WasPressedThisFrame() && Time.timeScale == 1)
        {
            StartCoroutine(MostrarMenuPausa());
        }    
        else if(abierto && playerInputs.MenuNav.PauseMenu.WasPressedThisFrame() && !menuVolumen.abierto)
        {
            StartCoroutine(OcultarMenuPausa());
        }
    }

    public void OcultarMenuPausaButton()
    {
        StartCoroutine (OcultarMenuPausa());
    }

    public void SalirButton()
    {
        Time.timeScale = 1f;
        GameController.instance.GoToMainMenu();
    }

    IEnumerator MostrarMenuPausa()
    {
        Time.timeScale = 0;
        for(int i = 0; i < 10; i++)
        {
            canvasGroup.alpha += .1f;
            yield return new WaitForSecondsRealtime(.05f);
        }
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        abierto = true;
    }

    IEnumerator OcultarMenuPausa()
    {
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        abierto = false;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }


}
