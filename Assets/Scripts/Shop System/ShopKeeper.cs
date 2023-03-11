using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private ShopItemList shopItemsHeld;
    [FormerlySerializedAs("_shopSystem")] [SerializeField] private ShopSystem shopSystem;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;

    private void Awake()
    {
        shopSystem = new ShopSystem(shopItemsHeld.Items.Count, shopItemsHeld.MaxAllowedGold, shopItemsHeld.BuyMarkUp, shopItemsHeld.SellMarkUp);

        Debug.Log($"Shopkeepers {GetComponent<UniqueID>().ID} stock:");
        foreach (var item in shopItemsHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName}: {item.Amount}");
            shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted with the shop keeper");
        var playerInv = GetComponent<PlayerInventoryHolder>();
        //if (playerInv == null) return;
        OnShopWindowRequested?.Invoke(shopSystem, playerInv);
    }
}
