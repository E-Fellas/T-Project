using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStamina : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI staminaText;
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

        staminaText.text = "Stamina: " + playerVariables.GetStaminaAtual();
        float fillValue = playerVariables.GetStaminaAtual() / playerVariables.staminaMaxima;
        slider.value = fillValue;
    }
}