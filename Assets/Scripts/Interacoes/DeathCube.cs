using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour , IInteragir
{
    public PlayerVariables playerVariables;
    //public Interacoes interacoes;

    public void Interagir()
    {
        Debug.Log("Voce interagiu com o cubo da morte!");
        playerVariables.ReceberDano(100);
    }
}