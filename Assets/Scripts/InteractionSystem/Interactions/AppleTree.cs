using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }
    public InventorySelector inventorySelector;
    public Inventory_Obj maca;

    public bool Interact(Interactor interactor)
    {
        inventorySelector.AddItem(maca, 1);
        Debug.Log("Colheu Maça!!");
        return true;
    }
}
