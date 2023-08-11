using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerVariables = collision.gameObject.GetComponent<PlayerVariables>();
            playerVariables.ReceberDano(playerVariables.vidaMaxima);
            playerVariables.PlayerRevive();
        }
    }
}
