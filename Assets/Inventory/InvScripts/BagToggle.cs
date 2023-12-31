using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BagToggle : MonoBehaviour
{
    public GameObject bag;
    public bool active = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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
