using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void Awake()
    {
        SaveLoad.OnLoadGameEvent += SpawnItems;
    }
    private void SpawnItems()
    {
        foreach (var item in SaveGameManager.data.activeItems)
        {
            //Debug.Log(item);
            Instantiate(item.Value.ItemData.ItemPrefab, item.Value.ItemPosition, Quaternion.identity);
        }
        Debug.Log("It's morbin time");
    }
}
