using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class InventorySlot : ISerializationCallbackReceiver // this represents a single slot of an inventory
{
    [NonSerialized] private InventoryItemData itemData; // reference to the data
    [SerializeField] private int stackSize; // current stack size of this slot
    [SerializeField] private int _itemID = -1;

    // public getters
    public InventoryItemData ItemData => itemData; 
    public int StackSize => stackSize;


    public InventorySlot(InventoryItemData source, int amount) // contructor to make an occupied slot
    {
        itemData = source;
        stackSize = amount;
        _itemID = itemData.ID;
    }
    public InventorySlot() // constructor to make an empty slot
    {
        ClearSlot();
    }

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

    public void UpdateInventorySlot(InventoryItemData data, int amount) // updates slot directly
    {
        itemData = data;
        stackSize = amount;
        _itemID = itemData.ID;
    }

    // Check if there is room left in target stack and output how much to full stack size
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize; // max stack size minus stack size is how much free space there is left
        return RoomLeftInStack(amountToAdd); // look below
    }

    // Check if there is room left in target stack
    public bool RoomLeftInStack(int amountToAdd) 
    {
        if (stackSize + amountToAdd <= itemData.MaxStackSize) return true; // if target stack size + amout to add is equals or less than max stack size there is room left in the stack
        //if (stackSize + amountToAdd <= 5) return true;
        //else return false; // else no room is left in the stack
        return false;
        
        
    }


    public void AddToStack(int amount) // adds an amount to stack
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount) // removes an amount to stack
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack) // splits the stack in two equal parts
    {
        if (stackSize <= 1) // if the stack is 1, don't split
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2); // split in half and round to integer
        RemoveFromStack(halfStack); // remove the half of the stack from inventory slot

        splitStack = new InventorySlot(itemData, halfStack); // create a copy of this slot with half the stack size
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(_itemID);
    }
}
