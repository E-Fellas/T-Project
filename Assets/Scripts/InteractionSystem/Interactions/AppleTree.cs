using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleTree : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    public InventorySelector inventorySelector;
    public Inventory_Obj item;
    public bool isHarvested = false;

    public bool Interact(Interactor interactor)
    {
        if (isHarvested == false)
        {
            inventorySelector.AddItem(item, 1);
            Debug.Log("Colheu Maça!!");
            isHarvested = true;
            return true;
        }
        else
        {
            Debug.Log("A árvore já foi colhida");
            return false;
        }
        
       
    }
}
