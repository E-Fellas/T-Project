using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto colidido tem tag player
        if (collision.gameObject.tag == "Player")
        {
            // Procura pelo script PlayerVariables no player
            var playerVariables = collision.gameObject.GetComponent<PlayerVariables>();

            // Da dano maximo no jogador e entao o revive
            playerVariables.ReceberDano(playerVariables.vidaMaxima);
            playerVariables.PlayerRevive();
        }
    }
}
