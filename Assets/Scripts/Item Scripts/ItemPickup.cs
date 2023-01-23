using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour
{
    public float PickupRadius = 1f;
    public InventoryItemData ItemData;
    public int stackSize = 1;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickupRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        Debug.Log($"ID: {ItemData.ID}\nDisplay Name: {ItemData.DisplayName}\nDescription: {ItemData.Description}\nMaximum Stack Size: {ItemData.MaxStackSize}");

        if (inventory.InventorySystem.AddToInventory(ItemData, stackSize))
        {
            Destroy(this.gameObject);
        }
    }
}
