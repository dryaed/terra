using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ShopSystem : MonoBehaviour
{
    [FormerlySerializedAs("_shopInventory")] [SerializeField] private List<ShopSlot> shopInventory;
    [FormerlySerializedAs("_availableGold")] [SerializeField] private int availableGold;
    [FormerlySerializedAs("_buyMarkUp")] [SerializeField] private float buyMarkUp;
    [FormerlySerializedAs("_sellMarkUp")] [SerializeField] private float sellMarkUp;
    
    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        availableGold = gold;
        this.buyMarkUp = buyMarkUp;
        this.sellMarkUp = sellMarkUp;
        
        SetShopSize(size);
    }

    private void SetShopSize(int size)
    {
        shopInventory = new List<ShopSlot>(size);

        for (int i = 0; i < size; i++)
        {
            shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData data, int amount)
    {
        if (ContainsItem(data, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(amount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(data, amount);
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out ShopSlot shopSlot) 
    {
        shopSlot = shopInventory.Find(i => i.ItemData == itemToAdd);
        return shopSlot != null; // if there is no such slot, return false
    }
}
