using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_RunTime : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI runTime_UI;
    public float runningTime = 0.0f;
    public void ResetRunTimeCounter()
    {
        runningTime = 0;
    }
    void Update()
    {
        if(playerVariables.GetestaVivo() == true)
        {
            runTime_UI.text =
                "Run Time \n" + runningTime.ToString("F2");
                runningTime += Time.deltaTime;
            
        }
        else
        {
            runTime_UI.text =
                "Run Time \n" + runningTime.ToString();
        }

       


    }
}