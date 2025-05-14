using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Referencias para os inputs
    public float inputHorizontal { get; private set; }
    public float inputVertical { get; private set; }
    public bool inputPulo { get; private set; }
    public bool inputCorrida { get; private set; }
    public bool inputDash { get; private set; }
    public bool inputReviver { get; private set; }
    public bool inputInteragir { get; private set; }
    public bool inputAbrirInventario { get; private set; }

    // Lógica para verificar inputs pressionados
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputPulo = Input.GetKeyDown(KeyCode.Space);
        inputCorrida = Input.GetKeyDown(KeyCode.RightShift);
        inputCorrida = Input.GetKeyDown(KeyCode.LeftShift);
        inputReviver = Input.GetKeyDown(KeyCode.K);
        inputInteragir = Input.GetKeyDown(KeyCode.E);
        inputAbrirInventario = Input.GetKeyDown(KeyCode.I);
    }
}
