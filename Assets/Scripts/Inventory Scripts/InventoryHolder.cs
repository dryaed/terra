using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize; // how many slots does this holder have
    [SerializeField] protected InventorySystem primaryInventorySystem; // which inventory system is this a part of

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem; // getter

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested; //thefuk

    protected virtual void Awake()
    {
        primaryInventorySystem = new InventorySystem(inventorySize); 
    }
}
