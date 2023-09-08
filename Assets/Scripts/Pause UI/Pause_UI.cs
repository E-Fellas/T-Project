using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_UI : MonoBehaviour
{
    public static bool GamePause = false;

    public GameObject PauseUI;

    public Slider volumeSlider;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
    PauseUI.SetActive(true);
    Time.timeScale = 0f;
    GamePause = false;
    
    }

    void Pause()
    {
    PauseUI.SetActive(false);
    Time.timeScale = 1f;
    GamePause = true;
    }
}