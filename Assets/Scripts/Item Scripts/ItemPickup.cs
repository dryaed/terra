using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour 
{
    public float PickupRadius = 1f; // pickup radius
    public InventoryItemData ItemData; // what is the item
    public int stackSize = 1; // how many of the item is there

    private SphereCollider myCollider; // sphere collider that is used to detect the player

    private void Awake() // creates the detection sphere
    {
        myCollider = GetComponent<SphereCollider>(); 
        myCollider.isTrigger = true;
        myCollider.radius = PickupRadius;
    }

    private void OnTriggerEnter(Collider other) // this gets called when something collides with the item in the world
    {
        var inventory = other.transform.GetComponent<InventoryHolder>(); // gets the inventory of the object that collided into the item

        if (!inventory) return; // if there is no inventory, ignore it

        //Debug.Log($"ID: {ItemData.ID}\nDisplay Name: {ItemData.DisplayName}\nDescription: {ItemData.Description}\nMaximum Stack Size: {ItemData.MaxStackSize}");

        if (inventory.InventorySystem.AddToInventory(ItemData, stackSize)) //if it is able to add this item, then ...
        {
            Destroy(this.gameObject); // remove the in-world item
        }
    }
}
