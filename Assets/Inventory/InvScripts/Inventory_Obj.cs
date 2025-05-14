using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory Item Object ")]
public class Inventory_Obj : ScriptableObject
{
    public int id;
    public string nome;
    public string description;
    public bool consumable = false;
    public bool werable = false;
    public Sprite icon;

    public virtual void Use()
    {
        Debug.Log($"Item {nome} utilizado");
    }
}
