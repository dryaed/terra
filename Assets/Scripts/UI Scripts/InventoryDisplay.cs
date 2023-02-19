using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using MouseButton = UnityEngine.UIElements.MouseButton;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary; // pair up UI slots and data slots
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem inventoryToDisplay); // for child classes

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot) // Slot value (system data)
            {
                slot.Key.UpdateUISlot(updatedSlot); // Slot key (ui representation of system data)
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftCtrlKey.isPressed; // check if the LShift is pressed

        // Clicked slot has an item & mouse is empty = pick up item

        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            if(isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) // split stack in half if LShift is pressed
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            } else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot); // copy clicked slot to mouse slot
                clickedUISlot.ClearUISlot(); // clear clicked slot
                return;
            }          
        }

        // Clicked slot is empty & mouse has an item = place item
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            // Shift click to take half

            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot); // copy mouse slot to clicked slot
            clickedUISlot.UpdateUISlot(); // update ui
            mouseInventoryItem.ClearUISlot(); // clear mouse slot
            return;
        }
        // Clicked slot has an item & mouse has an item
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            bool isSameItemData = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData; // check if clicked slot and mouse slot have the same item

            // Both items are the same and there is room in the stack
            if (isSameItemData && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot); // add mouse slot to clicked slot (their combination is not larger than max stack)
                clickedUISlot.UpdateUISlot(); // update ui

                mouseInventoryItem.ClearUISlot(); // clear mouse
                return;
            }
            // Both items are the same and there is NO room in the stack
            else if (isSameItemData && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedUISlot); // if clicked slot has no room left, swap clicked slot with mouse slot
                else // clicked slot is not max, so fill it with mouse slot
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack; // remainingOnMouse is how large the mouse slot stack will be after transfering items to clicked slot
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack); // fill clicked slot stack with mouse slot stack
                    clickedUISlot.UpdateUISlot(); // update ui

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse); // new item is the new mouse slot after the transfer
                    mouseInventoryItem.ClearUISlot(); // clear mouse slot
                    mouseInventoryItem.UpdateMouseSlot(newItem); // add the new item
                    return;
                }
            }           
            // Different items? Swap places
            else if (!isSameItemData)
            {
                SwapSlots(clickedUISlot);

                return;
            }
        }


       
        
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize); // clone mouse slot
        mouseInventoryItem.ClearUISlot(); // clear mouse slot

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot); // copy clicked slot to mouse slot

        clickedUISlot.ClearUISlot(); // clear clicked slot
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot); // copy cloned mouse slot to clicked slot
        clickedUISlot.UpdateUISlot(); // update ui
    }
}
