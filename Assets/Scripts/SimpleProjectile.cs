using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = -2f;
    public int damage = 100;

    private PlayerVariables playerVariables;

    private void Start()
    {
        playerVariables = PlayerVariablesSingleton.GetPlayerVariables();
    }

    private void Update()
    {
        // Move o proj�til ao longo do eixo Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
    }
}
