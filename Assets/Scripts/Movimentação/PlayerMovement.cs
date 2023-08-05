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
    public float distanciaChao = 0.2f;
    public LayerMask groundMask;
    private Vector3 forcaGravidade;
    public bool disableMovement = false;

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

        contatoChao = Physics.CheckSphere(transform.position, distanciaChao, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && contatoChao)
        {
            forcaGravidade.y = alturaPulo;
        }

        forcaGravidade.y += gravitForce * Time.deltaTime;
        controller.Move(forcaGravidade * Time.deltaTime);
    }

}
