using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ResourceNode : MonoBehaviour
{
    // Node parameters
    [SerializeField] private InventoryItemData resourceItemData;
    [SerializeField] private GameObject resourceNodePrefab;
    [SerializeField] private int resourceNodeSize;
    [SerializeField] private string toolTypeRequired;
    
    private GameObject _player; // player reference
    private GameObject _hotbarReference;
    
    private int _resourceLeft; // this keeps track of how many units is left in the node

    // Saving data
    private string _id;
    private ResourceNodeSaveData _saveData;

    public int AmountLeft
    {
        get => _resourceLeft;
        set => _resourceLeft = value;
    }

    private void Awake()
    {
        _saveData = new ResourceNodeSaveData(resourceItemData, resourceNodePrefab, _resourceLeft, transform.position, transform.rotation);
        SaveLoad.OnLoadGame += LoadResourceNodeData;
    }
    
    private void Start()
    {
        _resourceLeft = resourceNodeSize;
        _player = GameObject.Find("First Person Player"); // gets the player game object
        _hotbarReference = GameObject.Find("Player Hotbar");
        
        _id = GetComponent<UniqueID>().ID;
        SaveGameManager.data.activeResourceNodes.Add(_id, _saveData);
    }

    public void Interact()
    {
        var inventory = _player.GetComponent<PlayerInventoryHolder>(); // gets the inventory of the player
        var hotbar = _hotbarReference.GetComponent<HotbarDisplay>();

        if (!inventory) return; // if there is no inventory, ignore it

        if (!hotbar) return; // if there is no hotbar, ignore it

        if (toolTypeRequired == "none")
        {
            inventory.AddToInventory(resourceItemData, 1);
            _resourceLeft--;
        
            Debug.Log($"Resources Left: {_resourceLeft}");

            if (_resourceLeft <= 0)
            {
                SaveGameManager.data.depletedResourceNodes.Add(_id);
                Destroy(this.gameObject);
            }
            return;
        }

        if (hotbar.ToolItem.ToolType == null) return;
        if (hotbar.ToolItem.ToolType != toolTypeRequired) return;
        
        if (hotbar != null) Debug.LogWarning($"Tool type in hand: {hotbar.ToolItem.ToolType}");
        Debug.LogWarning($"Tool type required: {toolTypeRequired}");

        inventory.AddToInventory(resourceItemData, 1);
        _resourceLeft--;
        
        Debug.Log($"Resources Left: {_resourceLeft}");

        if (_resourceLeft <= 0) Destroy(this.gameObject);
    }
    
    private void LoadResourceNodeData(SaveData data)
    {
        Destroy(this.gameObject);
    }

    
    private void OnDestroy()
    {
        if (SaveGameManager.data.activeResourceNodes.ContainsKey(_id)) SaveGameManager.data.activeResourceNodes.Remove(_id);
        SaveLoad.OnLoadGame -= LoadResourceNodeData;
    }
}

[System.Serializable]
public struct ResourceNodeSaveData
{
    public InventoryItemData ItemData;
    public GameObject ResourceNodePrefab;
    public int ResourceLeft;
    public Vector3 ItemPosition;
    public Quaternion ItemRotation;

    public ResourceNodeSaveData(InventoryItemData _data, GameObject _resourceNodePrefab, int _resourceLeft, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        ResourceNodePrefab = _resourceNodePrefab;
        ResourceLeft = _resourceLeft;
        ItemPosition = _position;
        ItemRotation = _rotation;
    }

}