using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventorySelector;

public class InventoryBagHandler : MonoBehaviour
{
    public Transform inventoryParent;
    public static InventoryBagHandler instance;
    public UiHandler[] uiHandler;

    public InventorySelector inventorySelector;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateTextUI(Inventory_Obj addItem, int quantity)
    {
        List<InventoryItem> items = inventorySelector.GetItems();
        InventoryItem existingItem = items.Find(i => i.item.id == addItem.id);

        for (int i = 0; i < uiHandler.Length; i++)
        {
            if (addItem.id == uiHandler[i].itemId)
            {
                uiHandler[i].quantity.text = Convert.ToString(items.Find(i => i.item.id == addItem.id).quantity);
            }
        }
    }

    public void AddItemUI(Inventory_Obj addItem, int quantity)
    {
        Debug.Log("Entrou na UI"); 
        List<InventoryItem> items = inventorySelector.GetItems();
        InventoryItem existingItem = items.Find(i => i.item.id == addItem.id);

        uiHandler[inventorySelector.bagCount].AddItem(addItem);

        //Da Update na UI
        for (int i = 0; i < uiHandler.Length; i++)
        {
            if (addItem.id == uiHandler[i].itemId)
            {
                uiHandler[i].quantity.text = Convert.ToString(items.Find(i => i.item.id == addItem.id).quantity);
            }
        }

        Debug.Log("Adicionado a lista o item: " + addItem.nome + "\nquantidade: " + quantity);

        //pra mostrar na UI
        string texto = "Quantidade atual: " + items.Find(i => i.item.id == addItem.id).quantity.ToString();
        Debug.Log(texto);
    }

    public void RemoveItemUI(Inventory_Obj removeItem, int quantity)
    {
        List<InventoryItem> items = inventorySelector.GetItems();

        //procura este item no inventário
        InventoryItem existingItem = items.Find(i => i.item.id == removeItem.id);

        //Da Update na UI
        for (int i = 0; i < uiHandler.Length; i++)
        {
            if (removeItem.id == uiHandler[i].itemId)
            {
                uiHandler[i].quantity.text = Convert.ToString(items.Find(i => i.item.id == removeItem.id).quantity);
            }
        }

        //aqui, se zerar a quantidade daquele item, ele sai do inventário
        if (existingItem.quantity == 0)
        {
            for (int i = 0; i < uiHandler.Length; i++)
            {
                if (uiHandler[i].itemId == removeItem.id)
                {
                    items.Remove(existingItem);
                    uiHandler[i].ClearSlot();
                    HandleEmptySlot(i);
                }
            }

            inventorySelector.bagCount--;
        }
    }

    public void HandleEmptySlot(int position)
    {
        List<InventoryItem> items = inventorySelector.GetItems();

        for (int i = 0; i < 4 - position; i++)
        {
            Inventory_Obj helper = new Inventory_Obj();
            try
            {
                helper = uiHandler[i + position + 1].item;

                uiHandler[i + position].quantity.text = Convert.ToString(items.Find(i => i.item.id == helper.id).quantity);
                uiHandler[i + position].item = helper;
                uiHandler[i + position].AddItem(helper);

                uiHandler[i + position + 1].ClearSlot();
            }
            catch
            {
                return;
            }
        }
    }
    public void Start()
    {
        uiHandler = inventoryParent.GetComponentsInChildren<UiHandler>();
    }
}
