using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder
{
    public void Niggers()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem); // calls the DisplayInventory() using the primaryInventorySystem of this specific object 
    }
}
