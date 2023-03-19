using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : Interactable
{
    public override string GetDescription()
    {
        string output = "Press [E] to open this chest."; //interaction hint
        return output;
    }

    public override void Interact() // this is weird way to call the Interact() function inside of ChestInventory class, because the inventoryHolder is not accessable outside of InventoryHolder classes
    {
        ChestInventory inventory = GetComponent<ChestInventory>(); 
        inventory.Interact();
    }    
}
