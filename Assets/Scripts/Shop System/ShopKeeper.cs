using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private ShopItemList shopItemsHeld;
    private ShopSystem _shopSystem;

    private void Awake()
    {
        _shopSystem = new ShopSystem(shopItemsHeld.Items.Count, shopItemsHeld.MaxAllowedGold, shopItemsHeld.BuyMarkUp, shopItemsHeld.SellMarkUp);

        Debug.Log($"Shopkeepers {GetComponent<UniqueID>().ID} stock:");
        foreach (var item in shopItemsHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName}: {item.Amount}");
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }
}
