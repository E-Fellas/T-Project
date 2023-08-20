using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ScreenHealth : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI healthText;
    
    void Update()
    {
        healthText.text =
            "Health: " + playerVariables.GetvidaAtual() +
            "\nStamina: " + playerVariables.GetStaminaAtual();
        ;
    }
}
