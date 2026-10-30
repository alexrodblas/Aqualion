using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsTextController : MonoBehaviour
{

    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoins(int coins)
    {
        text.text = coins.ToString();
    }
}
