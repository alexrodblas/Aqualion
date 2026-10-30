using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextController : MonoBehaviour
{
    TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = GameController.instance.getConfig().playerHealth.ToString();
    }

    public void UpdateHealth(int health)
    {
        text.text = health.ToString();
    }
}