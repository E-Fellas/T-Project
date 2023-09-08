using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantity;

    public Inventory_Obj item;

    public int ConsumableCount = 0;
    public int InventoryCount = 0;
    public int itemId;

    public void AddItem (Inventory_Obj newItem)
    {
        itemId = newItem.id;

        if (newItem.consumable && ConsumableCount <= 4)
            HotbarAddItem(newItem);
        else
            InventoryAddItem(newItem);

    }

    public void ClearSlot()
    {
        item = null;
        itemId= 0;

        icon.sprite= null;
        icon.enabled= false;
        quantity.text = null;

        ConsumableCount--;
    }

    public void HotbarAddItem (Inventory_Obj newItem)
    {
        ConsumableCount++;
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void InventoryAddItem(Inventory_Obj newItem)
    {
        throw new NotImplementedException();
    }

    public int GetQuantity()
    {
        return Convert.ToInt16(quantity.text);
    }
}
