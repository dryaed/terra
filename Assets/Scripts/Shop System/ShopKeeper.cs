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
    [SerializeField] private ShopSystem shopSystem;
    private PlayerInventoryHolder _playerInventory;

    private string _id;
    private ShopSaveData _shopSaveData;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;

    private void Awake()
    {
        shopSystem = new ShopSystem(shopItemsHeld.Items.Count, shopItemsHeld.MaxAllowedGold, shopItemsHeld.BuyMarkUp, shopItemsHeld.SellMarkUp);
        
        if (GameObject.Find("First Person Player").TryGetComponent<PlayerInventoryHolder>(out _playerInventory)) Debug.Log("Player Inventory Holder found for ShopKeeper");
        
        Debug.Log($"Shopkeepers {GetComponent<UniqueID>().ID} stock:");
        foreach (var item in shopItemsHeld.Items)
        {
            Debug.Log($"{item.ItemData.DisplayName}: {item.Amount}");
            shopSystem.AddToShop(item.ItemData, item.Amount);
        }

        _id = GetComponent<UniqueID>().ID;
        _shopSaveData = new ShopSaveData(shopSystem);
    }

    private void Start()
    {
        if (!SaveGameManager.data.shopKeeperDictionary.ContainsKey(_id)) SaveGameManager.data.shopKeeperDictionary.Add(_id, _shopSaveData);;
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void OnDisable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void LoadInventory(SaveData data)
    {
        if (!data.shopKeeperDictionary.TryGetValue(_id, out ShopSaveData shopSaveData)) return;

        _shopSaveData = shopSaveData;
        shopSystem = _shopSaveData.ShopSystem;
    }

    public void Interact()
    {
        Debug.Log("Interacted with the shop keeper");
        //if (playerInv == null) return;
        OnShopWindowRequested?.Invoke(shopSystem, _playerInventory);
    }
}

[System.Serializable]
public class ShopSaveData
{
    public ShopSystem ShopSystem;

    public ShopSaveData(ShopSystem shopSystem)
    {
        ShopSystem = shopSystem;
    }
}
