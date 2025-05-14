using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/Apple")]
public class Apple_Obj : Inventory_Obj
{
    private PlayerVariables playerVariables;
    public int healAmount = 10;

    public override void Use()
    {
        if (playerVariables == null)
        {
            playerVariables = PlayerVariablesSingleton.GetPlayerVariables();
        }

        Debug.Log("Entrou no use da Maça");
        playerVariables.Heal(healAmount);
    }
}
