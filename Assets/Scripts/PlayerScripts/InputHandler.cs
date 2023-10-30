using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InventorySelector inventorySelector;

    //apenas para teste
    public Inventory_Obj maca;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Input.GetKeyUp(KeyCode.P))
        {
            inventorySelector.AddItem(maca, 1);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            inventorySelector.AddItem(maca, 1);
        }
    }
}
