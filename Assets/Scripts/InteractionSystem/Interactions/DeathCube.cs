using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour , IInteractable
{
    public PlayerVariables playerVariables;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public string Interact(Interactor interactor)
    {
        Debug.Log("Voce interagiu com o cubo da morte!");
        playerVariables.ReceberDano(100);
        return "Voce interagiu com o cubo da morte!";
    }
}