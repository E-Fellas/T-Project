using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaodePressao : MonoBehaviour
{
    public GameObject SimpleProjectile;
    public bool projectileOn = false;

    private Vector3 initialPlatePosition;
    public float plateSpeed = 2.0f;
    private Vector3 targetPlatePosition;

    System.Random rnd = new System.Random();

    private void Start()
    {
        initialPlatePosition = transform.position;
        targetPlatePosition = initialPlatePosition;
    }

    private void Update()
    {
        // Atualiza a posição da placa suavemente usando Lerp
        transform.position = Vector3.Lerp(transform.position, targetPlatePosition, plateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o jogador
        if (collision.gameObject.CompareTag("Player") && !projectileOn)
        {
            print("Boa sorte!!!");

            targetPlatePosition = initialPlatePosition - Vector3.up * 0.5f; // Desce a plaa
            projectileOn = true;
            StartCoroutine(ProjectileRoutine());
        }
    }

    IEnumerator ProjectileRoutine()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            ProjectileXAssis();
            yield return new WaitForSeconds(2);
        }

        projectileOn = false;
        voltaPlaca();
    }

    private void ProjectileXAssis()
    {
        int num = rnd.Next(0, 20);
        Debug.Log(num);

        for (int i=0; i < 20; i++) 
        {
            if (i != num && i != num - 1 && i != num + 1)
                CriarSimpleProjectile(-i - 10);
        }
    }

    private void CriarSimpleProjectile(float x)
    {
        if (SimpleProjectile != null)
        {
            // Define a posição onde o projétil será criado
            Vector3 spawnPosition = new Vector3(x, 4f, 9f);

            // Cria o projétil na posição especificada
            Instantiate(SimpleProjectile, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab do projétil não atribuído!");
        }
    }

    public void voltaPlaca()
    {
        targetPlatePosition = initialPlatePosition; // Volta a placa para a posição inicial
    }
}
