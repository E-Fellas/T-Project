using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleTree : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public InventorySelector inventorySelector;
    public Inventory_Obj item;
    public bool isHarvested = false;

    public string Interact(Interactor interactor)
    {
        if (isHarvested == false)
        {
            inventorySelector.AddItem(item, 1);
            Debug.Log("Colheu Maça!!");
            isHarvested = true;
            return "Colheu Maça!!";
        }
        else
        {
            Debug.Log("A árvore já foi colhida");
            return "A árvore já foi colhida";
        }
        
       
    }
}
