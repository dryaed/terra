using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

[System.Serializable]
public class InventorySystem // The full inventory system that contains holders
{
    [SerializeField] private List<InventorySlot> inventorySlots; // list of inventory slots

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size) // constructor that sets the inventory size
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    // this doesn't account for splitting picked up stacks properly, so if a picked up stack can't be added to an existing one without any leftovers it will just place it in a new slot
    // don't forget to subtract from the inworld item when the stack is more than what the player can take!!!
    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) // add an item to the inventory
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot)) // checks if there is a slot with the same item
        {
            foreach (var slot in inventorySlot) // for every slot in the inventory
            {
                if (slot.RoomLeftInStack(amountToAdd)) // if there is enough room left, add the item
                {
                    slot.AddToStack(amountToAdd); // add to the slot
                    OnInventorySlotChanged?.Invoke(slot); // update ui
                    return true;
                }
            }           
        }
        
        if (HasFreeSlot(out InventorySlot freeSlot)) // finds the first available free slot
        {
            //if (freeSlot.RoomLeftInStack(amountToAdd)) // if there is enough room left, add the item
            //{
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd); // sets the slot to what we wanted to add
                OnInventorySlotChanged?.Invoke(freeSlot); //update ui
                return true;
            //}

            
        }
        return false; // can't add item to inventory
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> inventorySlot) // searches for a slot that has the same itemData and returns it
    {
        inventorySlot = InventorySlots.Where(slot => slot.ItemData == itemToAdd).ToList(); // get a list of inventory slots in InventorySlots that have the same ItemData (Linq magic)

        return inventorySlot == null ? false : true; // if there is no such slot, return false
    }

    public bool HasFreeSlot(out InventorySlot freeSlot) // searches for a slot that has no ItemData in it
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null); // get first free slot
        return freeSlot == null ? false : true; // if there is no free slot, return false
    }
}
