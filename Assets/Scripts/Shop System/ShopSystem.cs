using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> shopInventory;
    [SerializeField] private int availableGold;
    [SerializeField] private float buyMarkUp;
    [SerializeField] private float sellMarkUp;

    public List<ShopSlot> ShopInventory => shopInventory;
    public int AvailableGold => availableGold;
    public float BuyMarkUp => buyMarkUp;
    public float SellMarkUp => sellMarkUp;
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
            return;
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
    
    // the SHOP is PURCHASING the item from the player
    public void PurchaseItem(InventoryItemData data, int amount)
    {
        if (!ContainsItem(data, out ShopSlot slot)) return;
        
        slot.RemoveFromStack(amount);
    }
    
    // the PLAYER is SELLING the item to the shop
    public void SellItem(InventoryItemData kvpKey, int kvpValue, int price)
    {
        AddToShop(kvpKey, kvpValue);
        ReduceGold(price);
    }

    public void GainGold(int basketTotal)
    {
        availableGold += basketTotal;
    }
    public void ReduceGold(int price)
    {
        availableGold -= price;
    }
}
