using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{    
    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame) OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.InventorySystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InventorySystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) // add an item to the inventory
    {
        if (primaryInventorySystem.AddToInventory(itemToAdd, amountToAdd)) // try to add to hotbar
        {
            return true;
        }

        return false; // can't add item to inventory
    }
}
