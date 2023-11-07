using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariablesSingleton : MonoBehaviour
{
    private static PlayerVariablesSingleton instance;
    public PlayerVariables playerVariables;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PlayerVariables GetPlayerVariables()
    {
        return instance.playerVariables;
    }
}
