using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaodePressao : MonoBehaviour
{
    public GameObject SimpleProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Placa de pressão pressionada!");

            CriarSimpleProjectile();
        }
    }

    private void CriarSimpleProjectile()
    {
        if (SimpleProjectile != null)
        {
            // Define a posição onde o projétil será criado
            Vector3 spawnPosition = new Vector3(-20f, 4f, 9f);

            // Cria o projétil na posição especificada
            Instantiate(SimpleProjectile, spawnPosition, Quaternion.identity);

            Debug.Log("Projétil criado.");
        }
        else
        {
            Debug.LogWarning("Prefab do projétil não atribuído!");
        }
    }
}
