using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicProjectileSc : MonoBehaviour
{
    private PlayerVariables playerVariables;
    public int damage = 100;

    private void Start()
    {
        playerVariables = PlayerVariablesSingleton.GetPlayerVariables();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causa dano ao jogador
            if (playerVariables.GetvidaAtual() >= 0)
            {
                playerVariables.ReceberDano(damage);
            }

            // Destroi o proj�til
            Destroy(gameObject);

            //print de debug
            print(playerVariables.GetvidaAtual());
        }
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
