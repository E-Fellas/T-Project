using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public UI_ShowText showText;

    void Start()
    {
        showText.ShowTextOnUI("Bem vindo! Use E para interagir com objetos.");
    }

}