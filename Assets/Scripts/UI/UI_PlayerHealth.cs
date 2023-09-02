using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI healthText;
    public Image fillImage;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        healthText.text = "Health: " + playerVariables.GetvidaAtual();            
        float fillValue = playerVariables.GetvidaAtual() / playerVariables.vidaMaxima;
        slider.value = fillValue;
    }
}
