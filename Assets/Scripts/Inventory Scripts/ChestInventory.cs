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

    private void LoadInventory(SaveData data)
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.SetPositionAndRotation(chestData.position, chestData.rotation);
        }
    }
}

[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem invSystem;
    public Vector3 position;
    public Quaternion rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
    {
        invSystem = _invSystem;
        position = _position;
        rotation = _rotation;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        invSystem = _invSystem;
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }
}
