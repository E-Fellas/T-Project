using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InventoryHandler : MonoBehaviour
{
    //Classe para dar tracking, nos itens e em sua quantidade
    [System.Serializable]
    public class InventoryItem
    {
        public Inventory_Obj item;
        public int quantity;
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(Inventory_Obj addItem, int quantity)
    {
        //procura este item no inventário
        InventoryItem existingItem = items.Find(i => i.item.id == addItem.id);

        //se ele existir, aumenta a quantidade, se não adiciona na lista
        if(existingItem != null)
            existingItem.quantity = quantity;
        else
            items.Add(new InventoryItem { item = addItem, quantity = quantity });
    }

    public void RemoveItem(Inventory_Obj removeItem, int quantity)
    {
        //procura este item no inventário
        InventoryItem existingItem = items.Find(i => i.item.id == removeItem.id);

        //se for nulo, não existem no inventário
        if(existingItem == null)
        {
            Debug.Log("Este item não existe no inventário" + removeItem.nome);
            return;
        }
        else
        {
            //se a quantidade que possui for menor que a que precisa, F
            if(quantity > existingItem.quantity)
            {
                Debug.Log("Quantidade do item insuficiente, possui:" + existingItem.quantity + " quer retirar:" + quantity);
                return;
            }
            else
            {
                existingItem.quantity -= quantity;
            }
        }

        //aqui, se zerar a quantidade daquele item, ele sai do inventário
        if(existingItem.quantity == 0)
            items.Remove(existingItem);
    }





}
