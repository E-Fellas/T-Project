using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ScreenStamina : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI staminaText;
    void Update()
    {
        staminaText.text = "Stamina: " + playerVariables.GetStaminaAtual();
    }
}
