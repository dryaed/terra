using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class InventorySlot : ItemSlot // this represents a single slot of an inventory
{
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
        return stackSize + amountToAdd <= itemData.MaxStackSize;
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
}
