using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory Item Object ")]
public class Inventory_Obj : ScriptableObject
{
    public int id;
    public string nome;
    public string description;
    public bool consumable;
    public bool werable;
    public Sprite icon;
}
