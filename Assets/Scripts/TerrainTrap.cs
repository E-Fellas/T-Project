using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TerrainTrap : MonoBehaviour
{
    public int damage;
    public float force;
    public bool destroyOnCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerVariables = collision.gameObject.GetComponent<PlayerVariables>();

            if (!playerVariables.GetestaVivo())
                return;

            var controller = collision.gameObject.GetComponent<CharacterController>();

            Vector3 direction = (collision.transform.position - transform.position).normalized;
            Vector3 knockback = direction * force;

            controller.Move(knockback);

            playerVariables.ReceberDano(damage);

            Debug.Log($"voce colidiu com o {gameObject.name} e tomou {damage}. \nVida atual: {playerVariables.GetvidaAtual()}");

            if (destroyOnCollision)
                Destroy(gameObject);
        }
    }
}
