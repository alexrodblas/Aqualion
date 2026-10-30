using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolverBoton : MonoBehaviour
{
    Button boton;
    private void Start()
    {
        boton = GetComponent<Button>();

        boton.onClick.AddListener(GameController.instance.GoToMainMenu);
    }
    private void OnDisable()
    {
        boton.onClick.RemoveListener(GameController.instance.GoToMainMenu);
    }
}
