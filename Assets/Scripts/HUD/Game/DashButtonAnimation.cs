using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButtonAnimation : MonoBehaviour
{
    [SerializeField] Image[] imagesAnim;
    int idx;
    bool animating;
    float cd;
    private void OnEnable()
    {
        idx = 0;
        animating = true;
    }

    private void OnDisable()
    {
        animating = false;
    }

    private void Update()
    {
        if (animating)
        {
            cd += Time.deltaTime;
        }

        if(cd >= 0.5f)
        {
            if(idx == 0) { 
            }
        }
    }
}
