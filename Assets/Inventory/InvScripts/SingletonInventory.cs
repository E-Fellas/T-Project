using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInventory : MonoBehaviour
{


    private static SingletonInventory instance;
    public InventoryHandler inventoryHandler;

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

    public static InventoryHandler GetInventoryHandler()
    {
        return instance.inventoryHandler;
    }
}
