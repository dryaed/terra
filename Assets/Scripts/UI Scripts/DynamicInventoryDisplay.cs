using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;
    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if (inventorySystem != null) inventorySystem.OnInventorySlotChanged += UpdateSlot;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        

        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (inventoryToDisplay == null) return;

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToDisplay.InventorySlots[i]);
            uiSlot.Init(inventoryToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }

    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            // it is better to switch to object pooling for this
            Destroy(item.gameObject);
        }
        if (slotDictionary != null) slotDictionary.Clear();
    }

    private void OnDisable()
    {
        if (inventorySystem != null) inventorySystem.OnInventorySlotChanged += UpdateSlot;
    }
}
