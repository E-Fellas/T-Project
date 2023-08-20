using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_RunTime : MonoBehaviour
{
    public PlayerVariables playerVariables;
    public TextMeshProUGUI runTime_UI;
    public float runningTime;
    void Update()
    {
        runTime_UI.text =
        "Run Time" + runningTime.ToString();


    }
}
