using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InventoryHotBarHandler : MonoBehaviour
{
    public Transform inventoryParent;
    public static InventoryHotBarHandler instance;
    public UiHandler[] uiHandler;

    //vizualição na UI
    public UI_ShowText uI_ShowText;

    //apenas para teste
    public Inventory_Obj teste;
    public Inventory_Obj banana;

    private void Awake()
    {
        instance = this;
    }

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
        if (existingItem != null)
        {
            existingItem.quantity += quantity;

            //Da Update na UI
            for (int i = 0; i < uiHandler.Length; i++)
            {
                if(addItem.id == uiHandler[i].itemId)
                {
                    uiHandler[i].quantity.text = Convert.ToString(items.Find(i => i.item.id == addItem.id).quantity);
                }
            }
        }
        else
        {
            //a linha abaixo é apenas para teste, deve ser modificado para algo mais seguro
            items.Add(new InventoryItem { item = addItem, quantity = quantity });
            uiHandler[items.Count -1].AddItem(addItem);
        }

        Debug.Log("Adicionado a lista o item: " + addItem.nome + "\nquantidade: " + quantity);

        //pra mostrar na UI
        string texto = "Quantidade atual: " + items.Find(i => i.item.id == addItem.id).quantity.ToString();
        Debug.Log(texto);
        uI_ShowText.ShowTextOnUI(texto);
    }

    public void RemoveItem(Inventory_Obj removeItem, int quantity)
    {
        //procura este item no inventário
        InventoryItem existingItem = items.Find(i => i.item.id == removeItem.id);

        //se for nulo, não existem no inventário
        if(existingItem == null)
        {
            Debug.Log("Este item não existe no inventário, item.nome: " + removeItem.nome);
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

                //Da Update na UI
                for (int i = 0; i < uiHandler.Length; i++)
                {
                    if (removeItem.id == uiHandler[i].itemId)
                    {
                        uiHandler[i].quantity.text = Convert.ToString(items.Find(i => i.item.id == removeItem.id).quantity);
                    }
                }
            }
        }

        //aqui, se zerar a quantidade daquele item, ele sai do inventário
        if(existingItem.quantity == 0)
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
        }

        //Isso é tudo Log, em um futuro pode ser excluído, está aqui apenas para teste, tanto a linha debaixo quanto o try catch
        Debug.Log("Removido da lista o item: " + removeItem.nome + "\nquantidade: " + quantity);
        try
        {
            Debug.Log("Quantidade atual: " + items.Find(i => i.item.id == removeItem.id).quantity);
        }
        catch
        {
            Debug.Log("Quantidade atual: 0");
            uI_ShowText.ShowTextOnUI("Cabo o item ;-;");
        }
    }

    public void HandleEmptySlot(int position)
    {
        for(int i = 0; i < 4-position; i++)
        {
            Inventory_Obj helper = new Inventory_Obj();
            try
            {
                helper = uiHandler[i + position +1].item;

                uiHandler[i + position].quantity.text = Convert.ToString(items.Find(i => i.item.id == helper.id).quantity);
                uiHandler[i + position].item = helper;
                uiHandler[i +position].AddItem(helper);

                uiHandler[i + position +1].ClearSlot();
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

    //Este Update deve ser retirado daqui, quando for realizado o Update de Inputs
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddItem(teste, 1);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            RemoveItem(teste, 1);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            AddItem(banana, 1);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            RemoveItem(banana, 1);
        }
    }
}
