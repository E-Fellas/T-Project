using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BagToggle : MonoBehaviour
{
    public InputHandler inputHandler;

    public GameObject bag;
    public bool active = true;
    //A bag deve começar ativo na UI, ou então vai dar OutOfBound assim que adicionar um item na bag!

    void Update()
    {
        if (inputHandler.inputAbrirInventario)
        {
            if (active)
            {
                Close();
            }
            else
            {
                Open();
            }
        }


        void Open()
        {
            bag.SetActive(true);
            Time.timeScale = 0f;
            active = true;

        }

        void Close()
        {
            bag.SetActive(false);
            Time.timeScale = 1f;
            active = false;
        }
    }
}
