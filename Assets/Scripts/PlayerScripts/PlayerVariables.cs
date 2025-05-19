using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public UI_RunTime runTime;
    public InputHandler inputHandler;

    public float vidaMaxima = 100;
    public float staminaMaxima = 100;
    public int numeroDeMortes = 0;
    public float sprintSpeed = 10f;
    public float dashSpeed = 5;
    public bool sprintOn = false;
    public bool isDashing  = false;
    private bool isInvulnerable = false;
    public float dashDuration = 0.01f;
    public float staminaDrain = 15f;

    private float vidaAtual;
    private float staminaAtual;
    private bool estaVivo = true;

    //posi��o inicial da cena
    public Vector3 posicaoRevive = new Vector3(0f, 2f, 0f);

    private void Start()
    {
        vidaAtual = vidaMaxima;
        staminaAtual = staminaMaxima;
        //playerMovement.moveSpeed  =  0;
    }

    private void Update()
    {
        if (inputHandler.inputReviver && !estaVivo)
        {
            PlayerRevive();
        } 

        if (inputHandler.inputDash && staminaAtual >= 20)
        {
            Dash();
        }
        Sprint();
    }

    public bool GetestaVivo()
    {
        return estaVivo;
    }

    public float GetvidaAtual()
    {
        return vidaAtual;
    }
    public float GetStaminaAtual()
    {
        return staminaAtual;
    }

    public bool GetisInvulnerable()
    {
        return isInvulnerable;
    }
    public void PlayerRevive()
    {
        //teleporta para o local de in�cio da cena
        StartCoroutine("TeleportePlayer");

        vidaAtual = vidaMaxima;
        staminaAtual = staminaMaxima;
        runTime.ResetRunTimeCounter();
        estaVivo = true;
    }

    public void ReceberDano(float dano)
    {
        if (!estaVivo || isInvulnerable)
            return;
        else
        {
            vidaAtual -= dano;
            AudioManager.instancia.Play("Damage");
        }

        if (vidaAtual <= 0 && estaVivo)
        {
            KillPlayer();
        }
    }

    public void Heal (float valor)
    {
        Debug.Log($"Healando em : {valor}");
        if (!estaVivo || isInvulnerable)
            return;
        else
        {
            if (vidaAtual + valor > vidaMaxima)
                vidaAtual = vidaMaxima;
            else
                vidaAtual += valor;
        }

        if (vidaAtual <= 0 && estaVivo)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        estaVivo = false;
        numeroDeMortes++;

        print("Voce morreu... pressione K para renascer!");
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
        if (inputHandler.inputCorrida && staminaAtual > 0 && estaVivo && !sprintOn)
        {
            sprintOn = true;
            playerMovement.moveSpeed = playerMovement.moveSpeed * 2f;

            staminaAtual -= staminaDrain * Time.deltaTime;

            if (staminaAtual <= 0)
            {
                staminaAtual = 0;
                sprintOn = false;
                playerMovement.moveSpeed = playerMovement.baseSpeed;
            }
        }
        else if (inputHandler.inputCorrida && staminaAtual > 0 && estaVivo && sprintOn)
        {
            float staminaDrain = 15f;
            staminaAtual -= staminaDrain * Time.deltaTime;

            if (staminaAtual <= 0)
            {
                staminaAtual = 0;
                sprintOn = false;
                playerMovement.moveSpeed = playerMovement.baseSpeed;
            }
        }
        else
        {
            if (sprintOn || playerMovement.moveSpeed != playerMovement.baseSpeed)
            {
                sprintOn = false;
                playerMovement.moveSpeed = playerMovement.baseSpeed;
            }
        }
    }

    public void Dash()
    {
        if (!isDashing && estaVivo)
        {
            isDashing = true;
            StartCoroutine(DashCoroutine());
            StartCoroutine(MakeInvulnerable(0.2f)); //Add MakeInvulnerable Function to playerVariables
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;    
        float originalMoveSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed *= 2.5f;// Fiz a velocidade ser equivalente a 2.5* a normal, mas podemos alterar
        float dashDistance = playerMovement.moveSpeed * dashDuration;
        Vector3 dashDirection = playerMovement.transform.forward;// Direção a frente da visão do personagem.
        float distanceTraveled = 0f;
        while (distanceTraveled < dashDistance)
        {
            float dashMove = playerMovement.moveSpeed * Time.deltaTime;
            playerMovement.controller.Move(dashDirection * dashMove);
            distanceTraveled += dashMove;
            yield return null;
        }
        playerMovement.controller.Move(dashDirection * (dashDistance - distanceTraveled));

        playerMovement.moveSpeed = originalMoveSpeed;
        isDashing = false;
        yield break;
    }
    public IEnumerator MakeInvulnerable(float invulnerableDuration)
    {        
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        isInvulnerable = false;
        yield break;
    }
}