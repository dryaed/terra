using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder
{
    public void Niggers()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0); // calls the DisplayInventory() using the primaryInventorySystem of this specific object 
    }

    private void Start()
    {
        var chestSaveData = new InventorySaveData(PrimaryInventorySystem, transform.position, transform.rotation);
        SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.InventorySystem;
            this.transform.SetPositionAndRotation(chestData.Position, chestData.Rotation);
        }
    }
}

