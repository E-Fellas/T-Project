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
    private bool isTextVisible = false;
    public void ShowTextOnUI(string text)
    {
        textToShow = text;
        showText_UI.text = textToShow;
        isTextVisible = true;
        timeElapsed = 0.0f;
        showText_UI.gameObject.SetActive(true);
    }
    void Update()
    {
        if (isTextVisible)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= displayTime)
            {
                showText_UI.text = null;
                isTextVisible = false;
                showText_UI.gameObject.SetActive(false);
            }
        }
    }


}
