using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreGameScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Image FondoImg;
    bool mostrandoCuentaAtras;
    [SerializeField] float tiempoMostrarMensaje;

    private void Awake()
    {
        textMeshProUGUI.text = "RECUERDA TU OBJETIVO: \n\nLLEGAR A LOS\n<color=red> <size=150>300 segundos </size></color>";
    }
    private void Start()
    {
        mostrandoCuentaAtras = false;
    }
    private void Update()
    {
        if(mostrandoCuentaAtras)
            return;

        if(tiempoMostrarMensaje > 0)
        {
            tiempoMostrarMensaje -= Time.deltaTime;
        }
        else
        {
            mostrandoCuentaAtras = true;
            StartCoroutine(MostrarCuentaAtras());
        }
    }
    IEnumerator MostrarCuentaAtras()
    {
        textMeshProUGUI.text = "preparado";
        yield return new WaitForSeconds(1f);

        for(int i = 3; i > 0; i--)
        {
            StartCoroutine(MostrarFondoCircular());
            textMeshProUGUI.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        GameController.instance.setIsGameStarted(true);
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator MostrarFondoCircular()
    {
        FondoImg.fillAmount = 1;
        for(int i = 0; i < 10; i++)
        {
            FondoImg.fillAmount -= .1f;
            yield return new WaitForSeconds(.1f);
        }
    }
}
