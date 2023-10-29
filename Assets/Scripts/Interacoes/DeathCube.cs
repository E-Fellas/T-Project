using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour , IInteractable
{
    public PlayerVariables playerVariables;

    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Voce interagiu com o cubo da morte!");
        playerVariables.ReceberDano(100);
        return true;
    }
}