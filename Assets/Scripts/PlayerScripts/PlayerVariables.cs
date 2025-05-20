using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public bool canDash = true;
    private bool isInvulnerable = false;
    public float dashDuration = 0.01f;
    public float staminaDrain = 15f;
    public float staminaRecover = 10f;
    public float staminaRecoverTime = 0f;

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
        RecoverStamina();
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

    private void RecoverStamina()
    {
        //Isso é pra por um delay entre gastar stamina e receber stamina, o delay é setado nos lugares que gasta stamina, atualmente nos métodos de Sprint e Dash
        if (staminaRecoverTime > 0)
        {
            staminaRecoverTime -= Time.deltaTime;
            return;
        }

        if (!sprintOn && !isDashing && staminaAtual < 100)
        {
            staminaAtual += staminaRecover * Time.deltaTime;

            if (staminaAtual > 100)
                staminaAtual = staminaMaxima;
        }
    }

    public void Sprint()
    {
        if (inputHandler.inputCorrida && staminaAtual > 0 && estaVivo && !sprintOn)
        {
            sprintOn = true;
            playerMovement.moveSpeed = playerMovement.moveSpeed * 2f;

            staminaAtual -= staminaDrain * Time.deltaTime;
            staminaRecoverTime = 2f;

            if (staminaAtual <= 0)
            {
                staminaAtual = 0;
                sprintOn = false;
                playerMovement.moveSpeed = playerMovement.baseSpeed;
            }
        }
        else if (inputHandler.inputCorrida && staminaAtual > 0 && estaVivo && sprintOn)
        {
            staminaAtual -= staminaDrain * Time.deltaTime;
            staminaRecoverTime = 2f;

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
        if (!isDashing && playerMovement.contatoChao && estaVivo && staminaAtual >= 20f && canDash)
        {
            isDashing = true;
            canDash = false; //Só controla o Cooldown do dash
            staminaAtual -= 20f;
            staminaRecoverTime = 2f;

            StartCoroutine(DashCoroutine());
            //StartCoroutine(MakeInvulnerable(0.2f)); //Add MakeInvulnerable Function to playerVariables
            StartCoroutine(DashSleep());
        }
    }

    IEnumerator DashCoroutine()
    {  
        float originalMoveSpeed = playerMovement.baseSpeed;
        float dashSpeed = originalMoveSpeed;
        dashSpeed *= 5f;// Fiz a velocidade ser equivalente a 2.5* a normal, mas podemos alterar
        float dashDistance = originalMoveSpeed * dashDuration;
        Vector3 dashDirection = playerMovement.transform.forward;// Direção a frente da visão do personagem.
        float distanceTraveled = 0f;
        while (distanceTraveled < dashDistance)
        {
            float dashMove = dashSpeed * Time.deltaTime;
            playerMovement.controller.Move(dashDirection * dashMove);
            distanceTraveled += dashMove;
            yield return null;
        }
        playerMovement.controller.Move(dashDirection * (dashDistance - distanceTraveled));

        //playerMovement.moveSpeed = originalMoveSpeed;
        isDashing = false;
        yield break;
    }

    IEnumerator DashSleep()
    {
        yield return new WaitForSeconds(0.5f); //timing do CD do dash
        canDash = true;
    }
    public IEnumerator MakeInvulnerable(float invulnerableDuration)
    {        
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        isInvulnerable = false;
        yield break;
    }
}