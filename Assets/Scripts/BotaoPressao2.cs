using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotaoPressao2 : MonoBehaviour
{
    public bool projectileOn = false;
    public Rigidbody projectile;
    public float projSpeed = -2;

    System.Random rnd = new System.Random();

    private void OnCollisionEnter(Collision collision)
    {
        
        // Verifica se colidiu com o jogador
        if (collision.gameObject.CompareTag("Player") && !projectileOn)
        {
            print("Boa sorte!!!");

            projectileOn = true;
            StartCoroutine(ProjectileCoroutine());
        }
    }

    IEnumerator ProjectileCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            ProjectileRoutineFoward();
            yield return new WaitForSeconds(1);
            ProjectileRoutineBackward();
            yield return new WaitForSeconds(1);
        }
    }

    public void ProjectileRoutineFoward()
    {
        int num = rnd.Next(0, 20);
        Debug.Log(num);

        for (int i = 0; i<19; i++)
        {
            if (i != num && i != num - 1 && i != num + 1)
            {
                spawnProjectile(-i - 11, 4f, -11f, 0, -2);
                Debug.Log("Teste " + i);
            }
        }
    }

    public void ProjectileRoutineBackward()
    {
        int num = rnd.Next(0, 20);
        Debug.Log(num);

        for (int i = 0; i < 19; i++)
        {
            if (i != num && i != num - 1 && i != num + 1)
            {
                spawnProjectile(-i - 11, 4f, -29f, 0, +2);
                Debug.Log("Teste " + i);
            }
        }
    }

    //Fica MUITO difícil trabalhar com as outras direções, de verdade
    /* 
    public void ProjectileRoutineRight()
    {
        int num = rnd.Next(0, 20);
        Debug.Log(num);

        for (int i = 0; i < 19; i++)
        {
            if (i != num && i != num - 1 && i != num + 1)
            {
                spawnProjectile(-i - 11, 4f, -11f, 1, -2);
                Debug.Log("Teste " + i);
            }
        }
    }

    public void ProjectileRoutineLeft()
    {
        int num = rnd.Next(0, 20);
        Debug.Log(num);

        for (int i = 0; i < 19; i++)
        {
            if (i != num && i != num - 1 && i != num + 1)
            {
                spawnProjectile(-i - 11, 4f, -11f, 1, 2);
                Debug.Log("Teste " + i);
            }
        }
    }
    **/

    public void spawnProjectile(float x, float y, float z, int direction, float projSpeed)
    {
        Vector3 v = new Vector3(x, y, z);//centro da direita = (-11f, 4, -21f)

        Rigidbody p = Instantiate(projectile, v, transform.rotation);

        if (direction == 0)
        {
            p.velocity = transform.forward * projSpeed;
        }
        else if (direction== 1)
        {
            p.velocity = transform.right * projSpeed;
        }

    }
}
