using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupInteraction : Interactable
{
    public InventoryItemData ItemData; // what is the item
    public int stackSize; // how many of the item is there
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("First Person Player"); // gets the player game object
    }

    public override string GetDescription() // displays next to the crosshair
    {
        //string output = "Press [E] to pick up this item.";
        string output = ItemData.DisplayName.ToString() + " x" + stackSize;
        return output;
    }

    public override void Interact()
    {
        var inventory = player.GetComponent<PlayerInventoryHolder>(); // gets the inventory of the player

        if (!inventory) return; // if there is no inventory, ignore it

        //Debug.Log($"ID: {ItemData.ID}\nDisplay Name: {ItemData.DisplayName}\nDescription: {ItemData.Description}\nMaximum Stack Size: {ItemData.MaxStackSize}");

        if (inventory.AddToInventory(ItemData, stackSize)) //if it is able to add this item, then ...
        {
            Destroy(this.gameObject); // remove the in-world item
        }
    }
    
}
