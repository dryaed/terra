using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize; // how many slots does this holder have
    [SerializeField] protected InventorySystem inventorySystem; // which inventory system is this a part of

    public InventorySystem InventorySystem => inventorySystem; // getter

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested; //thefuk

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize); 
    }
}
