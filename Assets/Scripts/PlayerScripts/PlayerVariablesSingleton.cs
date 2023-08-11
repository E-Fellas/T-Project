using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariableSingleton : MonoBehaviour
{
    //Este Script apenas existe para que objetos prefab tenham acesso ao Script PlayerVariables existente na cápsula (PlayerObject)


    private static PlayerVariableSingleton instance;
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
