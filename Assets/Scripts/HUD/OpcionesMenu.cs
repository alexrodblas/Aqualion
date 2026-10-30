using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;

    public bool abierto;

    private void Awake()
    {
        abierto = false;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = .0f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }

    public void MostrarCanvas()
    {
        abierto = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
    }

    public void OcultarCanvas()
    {
        abierto = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }



}
