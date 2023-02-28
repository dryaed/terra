using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize; // how many slots does this holder have
    [SerializeField] protected InventorySystem primaryInventorySystem; // which inventory system is this a part of
    [SerializeField] protected int offset = 10;

    public int Offset => offset;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem; // getter

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested; // event that fires when there is a need to display the dynamic inventory, takes in an inventory system and an offset (for the hotbar)

    protected virtual void Awake()
    {
        SaveLoad.OnLoadGame += LoadInventory;

        primaryInventorySystem = new InventorySystem(inventorySize); 
    }

    protected abstract void LoadInventory(SaveData saveData);
}
[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InventorySystem;
    public Vector3 Position;
    public Quaternion Rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
    {
        InventorySystem = _invSystem;
        Position = _position;
        Rotation = _rotation;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        InventorySystem = _invSystem;
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
    }
}

