using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{
    [NonSerialized] protected InventoryItemData itemData; // reference to the data
    [SerializeField] protected int stackSize; // current stack size of this slot
    [SerializeField] protected int _itemID = -1;

    // public getters
    public InventoryItemData ItemData => itemData; 
    public int StackSize => stackSize;
    
    public void ClearSlot() // clear this slot
    {
        itemData = null;
        stackSize = -1;
        _itemID = -1;
    }
    public void AssignItem(InventorySlot invSlot) // assign an item to an existing inventory slot
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stackSize); // if the slot containts the same item, then add to stack
        else // overwrite slot with invSlot
        {
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void AssignItem(InventoryItemData data, int amount)
    {
        if (itemData == data) AddToStack(amount);
        else
        {
            itemData = data;
            _itemID = data.ID;
            stackSize = 0;
            AddToStack(amount);
        }
    }
    
    public void AddToStack(int amount) // adds an amount to stack
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) // removes an amount to stack
    {
        stackSize -= amount;
        if (stackSize <= 0) ClearSlot();
    }
    
    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<ItemDatabase>("Database");
        itemData = db.GetItem(_itemID);
    }
}
