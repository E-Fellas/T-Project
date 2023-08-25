using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShowText : MonoBehaviour
{
    public TextMeshProUGUI showText_UI;

    private string textToShow;
    public float displayTime = 7.0f;
    private float timeElapsed = 0.0f;
    public void ShowTextOnUI(string text)
    {
        textToShow = text;
        showText_UI.text = textToShow;
    }
    void Update()
    {
        if (showText_UI.text != null)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= displayTime)
            {
                showText_UI.text = null;
            }
        }
    }



}
