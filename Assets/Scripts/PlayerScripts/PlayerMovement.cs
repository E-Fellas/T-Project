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
        }
    }

    public void handleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

        Vector3 capsuleCenter = transform.position + controller.center;
        float capsuleRadius = controller.radius;
        float capsuleHeight = controller.height -1.1f; 
        //eu legitimamente m�o sei pq eu tive que reduzir o tamanho da capsula pra funcionar... se alguem souber arrumar
        
        contatoChao = Physics.CheckCapsule(capsuleCenter, capsuleCenter + Vector3.down * capsuleHeight, capsuleRadius, groundMask);


        if (Input.GetKeyDown(KeyCode.Space) && contatoChao)
        {
            forcaGravidade.y = alturaPulo;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && infiniteJump)
        {
            forcaGravidade.y = alturaPulo;
        }

        forcaGravidade.y += gravitForce * Time.deltaTime;
        controller.Move(forcaGravidade * Time.deltaTime);
    }

}