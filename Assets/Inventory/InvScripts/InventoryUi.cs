using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    InventoryHandler inventory;


    // Start is called before the first frame update
    void Start()
    {
        inventory = SingletonInventory.GetInventoryHandler();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUi()
    {

    }
}
