using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerVariables playerVariables;
    public InputHandler inputHandler;
    public float moveSpeed = 8f;
    public float baseSpeed = 8f;
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
        float horizontal = inputHandler.inputHorizontal;
        float vertical = inputHandler.inputVertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Get the camera's transform
        Transform cameraTransform = Camera.main.transform;

        // Remove any y-axis movement from the camera's transform
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        // Calculate the direction based on the input and camera's rotation
        Vector3 calculatedDirection = vertical * cameraForward + horizontal * cameraTransform.right;

        if (calculatedDirection.magnitude >= 0.1f)
        {
            controller.Move(calculatedDirection * moveSpeed * Time.deltaTime);

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


        if (inputHandler.inputPulo && (contatoChao || infiniteJump))
        {
            forcaGravidade.y = alturaPulo;
        }

        forcaGravidade.y += gravitForce * Time.deltaTime;
        controller.Move(forcaGravidade * Time.deltaTime);
    }

}
