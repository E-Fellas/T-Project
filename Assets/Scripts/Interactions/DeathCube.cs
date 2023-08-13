using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour, IInteractable
{
    public PlayerVariables playerVariables;
    public void Interact()
    {
        Debug.Log("Voce interagiu com o cubo da morte!");
        playerVariables.ReceberDano(100);
    }
}
