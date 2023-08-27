using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public Image icon;

    Inventory_Obj item;

    public void AddItem (Inventory_Obj newItem)
    {
        item = newItem;

        icon.sprite= item.icon;
        icon.enabled= true;

    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite= null;
        icon.enabled= false;

    }
}
