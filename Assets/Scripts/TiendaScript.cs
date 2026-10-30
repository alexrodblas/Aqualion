using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaScript : MonoBehaviour
{
    private string[] frasesCapitan = 
    {  
        "RECUERDA QUE TU OBJETIVO ES LLEGAR A <color=red>300</color> SEGUNDOS!!!",
        "USA EL <color=red>DASH</color> CUANDO SEA NECESARIO",
        "EL FUTURO DE LA <color=red>HUMANIDAD</color> DEPENDE DE TI",
        "LAS MEJORES <color=red>ARMAS</color> SON LAS MAS <color=red> CARAS",
        "TODOS EN LA <color=red>TIERRA</color> ESTAMOS APOYANO",
        "No solo estas luchando contra <color=green>aliens</color>, estas defendiendo lo que significa ser humano.",
        "Eres nuestra <color=red>última esperanza</color>, no te rindas"
    };

    [SerializeField] EventReference compraFallida;

    [SerializeField] UISpriteAnimationRealTime animacionCapitan;
    [SerializeField] TextMeshProUGUI txtMensajeDesdeTierra;

    [SerializeField] GameObject avisoPulsaTienda;
    [SerializeField] CanvasGroup tienda;

    private PlayerInputs playerInputs;
    private bool pulsable;
    private bool abierta;


    private void Start()
    {
        playerInputs = PlayerController.instance.playerInputs;
        tienda.alpha = 0;
        pulsable = false;
        tienda.interactable = tienda.blocksRaycasts = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            pulsable = true;
            avisoPulsaTienda.gameObject.SetActive(true);
            avisoPulsaTienda.gameObject.GetComponent<UISpriteAnimation>().Func_PlayUIAnim();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null) 
        {
            if (avisoPulsaTienda != null)
            {
                pulsable = false;
                avisoPulsaTienda.gameObject.GetComponent<UISpriteAnimation>().Func_StopUIAnim();
                avisoPulsaTienda.gameObject.SetActive(false);
            }
        }
    }

    public bool getPulsable()
    {
        return pulsable;
    }

    private void Update()
    {
        if (pulsable && playerInputs.Shop.OpenShop.WasPressedThisFrame())
        {
            StartCoroutine(AbrirTienda());
        }
        else if(abierta && playerInputs.Shop.CloseShop.ReadValue<float>() != 0)
        {
            StartCoroutine(CerrarTienda());
        }

    }

    IEnumerator AbrirTienda()
    {
        if (animacionCapitan != null)
        {
            animacionCapitan.Func_PlayUIAnim();
            GenerarFraseAleatoria();
        }

        tienda.interactable = tienda.blocksRaycasts = true;
        Time.timeScale = 0;
        while(tienda.alpha < 1)
        {
            tienda.alpha += (float) 2 * Time.unscaledDeltaTime;
            yield return null;
        }
        abierta = true;
        tienda.alpha = 1;
    }

    private void GenerarFraseAleatoria()
    {
        int idxFrase = Random.Range(0,frasesCapitan.Length);
        txtMensajeDesdeTierra.text = frasesCapitan[idxFrase];
    }


    /// <summary>
    /// Funcion que se aplicara en un boton cuando la tienda esta abierta
    /// </summary>
    public void CerrarTiendaButton()
    {
        StartCoroutine(CerrarTienda());
    }

    IEnumerator CerrarTienda()
    {
        while (tienda.alpha > 0)
        {
            tienda.alpha -= (float) 0.5 * Time.unscaledDeltaTime;
            yield return null;
        }

        abierta = false;
        tienda.alpha = 0;
        Time.timeScale = 1;
        tienda.interactable = tienda.blocksRaycasts = false;

        if (animacionCapitan != null)
        {
            animacionCapitan.Func_StopUIAnim();
        }
    }

    public void TryToBuy(int weaponIdx)
    {
        int value = PlayerController.instance.weapons[weaponIdx].GetComponent<WeaponController>().weaponValue;
        if(SceneManager.GetActiveScene().name == "ControlesScene")
        {
            value = 0;
        }

        //Esto devuelve si tiene dinero para el arma seleccionada
        if (PlayerController.instance.getCoins() >= value)
        {
            PlayerController.instance.removeCoins(value);
            PlayerController.instance.SetWeapon(weaponIdx);
        }
        else
        {
            AudioController.instance.playOneShoot(compraFallida);
        }
    }
}
