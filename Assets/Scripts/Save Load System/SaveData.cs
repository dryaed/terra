using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //public PlayerData playerData;

    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickupSaveData> activeItems;

    public SerializableDictionary<string, InventorySaveData> chestDictionary;

    //temporary player save, later playerData will be used
    public InventorySaveData playerInventory;

    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickupSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        playerInventory = new InventorySaveData();
        //playerData = new PlayerData();
    }
}
