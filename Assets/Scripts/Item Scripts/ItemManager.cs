using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void Awake()
    {
        SaveLoad.OnLoadGameEvent += SpawnItems;
        SaveLoad.OnLoadGameEvent += SpawnResourceNodes;
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

    private void SpawnResourceNodes()
    {
        foreach (var item in SaveGameManager.data.activeResourceNodes)
        {
            //Debug.Log(item);
            var tempObj = Instantiate(item.Value.ResourceNodePrefab, item.Value.ItemPosition, Quaternion.identity);
            var newResourceNode = tempObj.GetComponent<ResourceNode>();
            newResourceNode.AmountLeft = item.Value.ResourceLeft;
        }
    }
}
