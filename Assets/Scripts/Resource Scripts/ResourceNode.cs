using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ResourceNode : MonoBehaviour
{
    // Node parameters
    [SerializeField] private InventoryItemData resourceItemData;
    //[SerializeField] private GameObject resourceNodePrefab;
    [SerializeField] private int resourceNodeSize;
    [SerializeField] private string toolTypeRequired;
    
    private GameObject _player; // player reference
    private GameObject _hotbarReference;
    
    [SerializeField] private int _resourceLeft; // this keeps track of how many units is left in the node

    // Saving data
    private string _id;
    private ResourceNodeSaveData _saveData;

    public string ToolTypeRequired => toolTypeRequired;
    public InventoryItemData ResourceItemData => resourceItemData;

    private void Awake()
    {
        _id = GetComponent<UniqueID>().ID;
        //_saveData = new ResourceNodeSaveData(resourceItemData, _resourceLeft, transform.position, transform.rotation);
        SaveLoad.OnLoadGame += LoadResourceNodeData;
        SaveLoad.OnSaveGame += SaveResourceNodeData;
    }

    private void SaveResourceNodeData()
    {
        _saveData = new ResourceNodeSaveData(resourceItemData, _resourceLeft, transform.position, transform.rotation);
        if (!SaveGameManager.data.resourceNodeDictionary.ContainsKey(_id)) SaveGameManager.data.resourceNodeDictionary.Add(_id, _saveData);
        else
        {
            SaveGameManager.data.resourceNodeDictionary.Remove(_id);
            SaveGameManager.data.resourceNodeDictionary.Add(_id, _saveData);
        }
    }

    private void Start()
    {
        _resourceLeft = resourceNodeSize;
        _player = GameObject.Find("First Person Player"); // gets the player game object
        _hotbarReference = GameObject.Find("Player Hotbar");
        //SaveResourceNodeData();
        //_id = GetComponent<UniqueID>().ID;
        //SaveGameManager.data.activeResourceNodes.Add(_id, _saveData);
        
        //if (!SaveGameManager.data.resourceNodeDictionary.ContainsKey(_id)) SaveGameManager.data.resourceNodeDictionary.Add(_id, _saveData);
    }

    public void Interact()
    {
        var inventory = _player.GetComponent<PlayerInventoryHolder>(); // gets the inventory of the player
        var hotbar = _hotbarReference.GetComponent<HotbarDisplay>();

        if (!inventory) return; // if there is no inventory, ignore it

        if (!hotbar) return; // if there is no hotbar, ignore it

        if (toolTypeRequired == "none")
        {
            
            Debug.Log($"Resources Left: {_resourceLeft}");
            if (_resourceLeft <= 0)
            {
                return;
                //SaveGameManager.data.depletedResourceNodes.Add(_id);
                //Destroy(this.gameObject);
            }
            _resourceLeft--;
            inventory.AddToInventory(resourceItemData, 1);
            return;
        }

        if (hotbar.ToolItem.ToolType == null) return;
        if (hotbar.ToolItem.ToolType != toolTypeRequired) return;
        
        if (hotbar != null) Debug.LogWarning($"Tool type in hand: {hotbar.ToolItem.ToolType}");
        Debug.LogWarning($"Tool type required: {toolTypeRequired}");
        
        

        if (_resourceLeft <= 0) return;
        _resourceLeft--;
        Debug.Log($"Resources Left: {_resourceLeft}");
        inventory.AddToInventory(resourceItemData, 1);
            //Destroy(this.gameObject); 
    }
    
    private void LoadResourceNodeData(SaveData data)
    {
        if (!data.resourceNodeDictionary.TryGetValue(_id, out ResourceNodeSaveData nodeSaveData)) return;
        
        _resourceLeft = nodeSaveData.ResourceLeft;
        //Destroy(this.gameObject);
    }

    
    private void OnDestroy()
    {
        //if (SaveGameManager.data.activeResourceNodes.ContainsKey(_id)) SaveGameManager.data.activeResourceNodes.Remove(_id);
        //SaveLoad.OnLoadGame -= LoadResourceNodeData;
    }
}

[System.Serializable]
public struct ResourceNodeSaveData
{
    public InventoryItemData ItemData;
    public int ResourceLeft;
    public Vector3 ItemPosition;
    public Quaternion ItemRotation;

    public ResourceNodeSaveData(InventoryItemData _data, int _resourceLeft, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        ResourceLeft = _resourceLeft;
        ItemPosition = _position;
        ItemRotation = _rotation;
    }

}