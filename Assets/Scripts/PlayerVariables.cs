using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public int vidaMaxima = 1000;
    public int staminaMaxima = 100;
    public int numeroDeMortes = 0;
    public float sprintSpeed = 10f;
    public bool sprintOn = false;

    public PlayerMovement playerMovement;

    private int vidaAtual;
    private int staminaAtual;
    private bool estaVivo = true;

    //posição inicial da cena
    public Vector3 posicaoRevive = new Vector3(0f, 2f, 0f);

    private void Start()
    {
        vidaAtual = vidaMaxima;
        staminaAtual = staminaMaxima;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !estaVivo)
        {
            PlayerRevive();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaAtual >= 50)
        {
            Sprint();
        }
    }

    public bool GetestaVivo()
    {
        return estaVivo;
    }

    public void PlayerRevive()
    {
        //teleporta para o local de início da cena
        StartCoroutine("TeleportePlayer");

        vidaAtual = vidaMaxima;
        staminaAtual = staminaMaxima;
        estaVivo = true;
    }

    public void ReceberDano(int dano)
    {
        if (!estaVivo)
            return;
        else
        {
            vidaAtual -= dano;
        }

        //código para matar o player.
        //local temporário!
        if (vidaAtual <= 0 && estaVivo)
        {
            estaVivo = false;
            numeroDeMortes++;

            print("Você morreu... pressione k para renascer!");
        }
    }

    IEnumerator TeleportePlayer()
    {
        playerMovement.disableMovement = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = posicaoRevive;
        yield return new WaitForSeconds(0.1f);
        playerMovement.disableMovement = false;

        Debug.Log("Revivendo...");
    }

    public void Sprint()
    {
        sprintOn = true;
        StartCoroutine("SprintCoroutine");
        staminaAtual = 0;
        sprintOn = false;
    }

    IEnumerator SprintCoroutine()
    {
        playerMovement.moveSpeed += sprintSpeed;
        yield return new WaitForSeconds(staminaAtual / 50f);
        playerMovement.moveSpeed -= sprintSpeed;
    }
}