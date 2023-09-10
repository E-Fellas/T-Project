using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_UI : MonoBehaviour
{
    public static bool GamePause = false;

    public GameObject PauseUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();
                Debug.Log("Set active false");
            } else
            {
                Pause();
                Debug.Log("Set active active");
            }
        }
    }

    void Resume()
    {
    PauseUI.SetActive(false);
    Time.timeScale = 1f;
    GamePause = false;
    
    }

    void Pause()
    {
    PauseUI.SetActive(true);
    Time.timeScale = 0f;
    GamePause = true;
    }
}