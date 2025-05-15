using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySelector : MonoBehaviour
{
    //apenas para teste
    public Inventory_Obj teste;
    public Inventory_Obj banana;

    public InventoryHotBarHandler hotBar;
    public InventoryBagHandler bag;

    public int barCount = 0;
    public int bagCount = 0;

    //Classe para dar tracking, nos itens e em sua quantidade
    [System.Serializable]
    public class InventoryItem
    {
        public Inventory_Obj item;
        public int quantity;
    }

    public List<InventoryItem> itemsBag = new List<InventoryItem>();
    public List<InventoryItem> itemsHotBar = new List<InventoryItem>();

    public List<InventoryItem> GetItems()
    {
        return itemsBag;
    }

    public List<InventoryItem> GetItemsHotBar()
    {
        return itemsHotBar;
    }

    public void AddItem(Inventory_Obj addItem, int quantity)
    {
        InventoryItem existingItem;
        if(addItem.consumable)
            existingItem = itemsHotBar.Find(i => i.item.id == addItem.id);
        else
            existingItem = itemsBag.Find(i => i.item.id == addItem.id);

        //se ele existir, aumenta a quantidade, se não adiciona na lista
        if (existingItem != null)
        {
            existingItem.quantity += quantity;
            if (addItem.consumable)
            {
                hotBar.UpdateTextUI(addItem, quantity);
            }
            else
            {
                bag.UpdateTextUI(addItem, quantity);
            }
        }
        else
        {
            if (addItem.consumable)
            {
                itemsHotBar.Add(new InventoryItem { item = addItem, quantity = quantity });
                hotBar.AddItemUI(addItem, quantity);
                barCount++;
            }
            else
            {
                itemsBag.Add(new InventoryItem { item = addItem, quantity = quantity });
                bag.AddItemUI(addItem, quantity);
                bagCount++;
            }
        }
    }

    public void RemoveItem(Inventory_Obj removeItem, int quantity)
    {
        InventoryItem existingItem;
        //procura este item no inventário
        if(removeItem.consumable)
            existingItem = itemsHotBar.Find(i => i.item.id == removeItem.id);
        else
            existingItem = itemsBag.Find(i => i.item.id == removeItem.id);

        //se for nulo, não existem no inventário
        if (existingItem == null)
        {
            Debug.Log("Este item não existe no inventário, item.nome: " + removeItem.nome);
            return;
        }
        else
        {
            //se a quantidade que possui for menor que a que precisa, F
            if (quantity > existingItem.quantity)
            {
                Debug.Log("Quantidade do item insuficiente, possui:" + existingItem.quantity + " quer retirar:" + quantity);
                return;
            }
            else
            {
                existingItem.quantity -= quantity;
            }

            if(removeItem.consumable)
            {
                hotBar.RemoveItemUI(removeItem, quantity);
            }
            else
            {
                bag.RemoveItemUI(removeItem, quantity);
            }
        }


        //Isso é tudo Log, em um futuro pode ser excluído, está aqui apenas para teste, tanto a linha debaixo quanto o try catch
        Debug.Log("Removido da lista o item: " + removeItem.nome + "\nquantidade: " + quantity);
        try
        {
            Debug.Log("Quantidade atual: " + itemsBag.Find(i => i.item.id == removeItem.id).quantity);
        }
        catch
        {
            Debug.Log("Quantidade atual: 0");
        }
    }

    //Este Update deve ser retirado daqui, quando for realizado o Update de Inputs
    // Apagar depois que testado
    private void Update()
    {
        HandleHotBarInput();

        if (Input.GetKeyUp(KeyCode.P))
        {
            AddItem(teste, 1);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            RemoveItem(teste, 1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddItem(banana, 1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveItem(banana, 1);
        }
    }
    private void HandleHotBarInput()
    {
        for (int i = 0; i < hotBar.uiHandler.Length; i++)
        {
            KeyCode key = KeyCode.Alpha1 + i; // KeyCode.Alpha1 (49) até Alpha5 (53), peguei do GPT mesmo, foda-se

            if (Input.GetKeyDown(key))
            {
                UiHandler slot = hotBar.uiHandler[i];

                if (slot.item != null)
                {
                    Debug.Log($"Usando item da HotBar: {slot.item.nome}");

                    UseItem(slot.item);
                    RemoveItem(slot.item, 1);
                }
                else
                {
                    Debug.Log($"Slot {i + 1} da HotBar está vazio.");
                }
            }
        }
    }

    private void UseItem(Inventory_Obj item)
    {
        item.Use();
    }
}
