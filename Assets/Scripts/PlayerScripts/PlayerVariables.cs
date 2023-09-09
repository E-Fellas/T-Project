using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public UI_RunTime runTime;

    public float vidaMaxima = 100;
    public float staminaMaxima = 100;
    public int numeroDeMortes = 0;
    public float sprintSpeed = 10f;
    public float dashSpeed = 5;
    public bool sprintOn = false;
    public bool isDashing  = false;

    public float dashDuration = 0.01f;

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
        if (Input.GetKeyDown(KeyCode.K) && !estaVivo)
        {
            PlayerRevive();
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && staminaAtual >= 50)
        {
            
            Sprint();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaAtual >= 20)
        {
            Dash();
        }        
        
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
        if (!estaVivo)
            return;
        else
        {
            vidaAtual -= dano;
            AudioManager.instancia.Play("Damage");
        }

        //c�digo para matar o player.
        //local tempor�rio!
        if (vidaAtual <= 0 && estaVivo)
        {
            estaVivo = false;
            numeroDeMortes++;

            print("Voc� morreu... pressione k para renascer!");
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
        staminaAtual = staminaAtual-20;
        sprintOn = false;
    }

    IEnumerator SprintCoroutine()
    {
        playerMovement.moveSpeed += sprintSpeed;
        yield return new WaitForSeconds(staminaAtual / 50f);
        playerMovement.moveSpeed -= sprintSpeed;
    }
    public void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;    
        float originalMoveSpeed = playerMovement.moveSpeed; // Store the current move speed to reset it after the dash remove
        playerMovement.moveSpeed *= 2.5f;
        float dashDistance = playerMovement.moveSpeed * dashDuration;
        Vector3 dashDirection = playerMovement.transform.forward; // You can change the direction as needed
        float distanceTraveled = 0f;
        while (distanceTraveled < dashDistance)
        {
            float dashMove = playerMovement.moveSpeed * Time.deltaTime;
            playerMovement.controller.Move(dashDirection * dashMove);
            distanceTraveled += dashMove;
            yield return null;
        }
        playerMovement.controller.Move(dashDirection * (dashDistance - distanceTraveled));

        //playerVariables.MakeInvulnerable(0.0f); Add MakeInvulnerable Function to playerVariables
        //yield return new WaitForSeconds(dashDuration);        // Wait for the dash duration
        playerMovement.moveSpeed = originalMoveSpeed;
        isDashing = false;
        yield break;
    }

}