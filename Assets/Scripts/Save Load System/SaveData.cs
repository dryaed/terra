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

    //public List<string> depletedResourceNodes;
    //public SerializableDictionary<string, ResourceNodeSaveData> activeResourceNodes;
    public SerializableDictionary<string, ResourceNodeSaveData> resourceNodeDictionary;

    public SerializableDictionary<string, InventorySaveData> chestDictionary; // stores chest data
    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public SaveData()
    {
        playerData = new PlayerData();
        playerInventory = new InventorySaveData();
        
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickupSaveData>();

        //depletedResourceNodes = new List<string>();
        //activeResourceNodes = new SerializableDictionary<string, ResourceNodeSaveData>();
        resourceNodeDictionary = new SerializableDictionary<string, ResourceNodeSaveData>();
        
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
    }
}
