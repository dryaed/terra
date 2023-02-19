using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) // add an item to the inventory
    {
        if (primaryInventorySystem.AddToInventory(itemToAdd, amountToAdd)) // try to add to hotbar
        {
            return true;
        } 
        else if (secondaryInventorySystem.AddToInventory(itemToAdd, amountToAdd)) // try to add to backpack
        {
            return true;
        }

        return false; // can't add item to inventory
    }
}
