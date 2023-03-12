using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerData playerData; // stores player data
    public InventorySaveData playerInventory;

    public List<string> collectedItems; // stores collected items to be deleted
    public SerializableDictionary<string, ItemPickupSaveData> activeItems; // stores items to be rendered in-world

    public SerializableDictionary<string, InventorySaveData> chestDictionary; // stores chest data
    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public SaveData()
    {
        playerData = new PlayerData();
        playerInventory = new InventorySaveData();
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickupSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
    }
}
