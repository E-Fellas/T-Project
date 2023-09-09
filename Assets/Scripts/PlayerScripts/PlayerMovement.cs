using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerVariables playerVariables;
    public float moveSpeed = 8f;
    public float gravitForce = -9f;
    public Vector3 lastMoveDirection = Vector3.forward;
    public float alturaPulo = 5f;

    private bool contatoChao;
    public float distanciaChao = -0.01f;
    public LayerMask groundMask;
    private Vector3 forcaGravidade;
    public bool disableMovement = false;

    //caso voc� queira testar o mapa, ative esta vari�vel.
    public bool infiniteJump = false;

    // Update is called once per frame
    void Update()
    {
        if (playerVariables.GetestaVivo() && !disableMovement)
        {
            handleMovement();
            handleJump();
        }
    }

    public void handleMovement()
    {
        if (!playerVariables.isDashing)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Vetor para angular a movimentacao para os angulos locais do Player
            Vector3 localDirection = transform.TransformDirection(direction);
            

            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * moveSpeed * Time.deltaTime); //Troque localDirection para direction para alterar entre vetores globais e locais

                if (controller.velocity != Vector3.zero) 
                {
                    Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
                    transform.rotation = Quaternion.LookRotation(horizontalVelocity.normalized);
                }
            }
        }
    }

    public void handleJump()
    {
        Vector3 capsuleCenter = transform.position + controller.center;
        float capsuleRadius = controller.radius;
        float capsuleHeight = controller.height - 1.1f;
        //eu legitimamente m�o sei pq eu tive que reduzir o tamanho da capsula pra funcionar... se alguem souber arrumar

        contatoChao = Physics.CheckCapsule(capsuleCenter, capsuleCenter + Vector3.down * capsuleHeight, capsuleRadius, groundMask);


        if (Input.GetKeyDown(KeyCode.Space) && contatoChao)
        {
            forcaGravidade.y = alturaPulo;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && infiniteJump)
        {
            forcaGravidade.y = alturaPulo;
        }

        forcaGravidade.y += gravitForce * Time.deltaTime;
        controller.Move(forcaGravidade * Time.deltaTime);
    }

}
