using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ResourceNode : MonoBehaviour
{
    // Node parameters
    [SerializeField] private InventoryItemData resourceItemData;
    [SerializeField] private int resourceNodeSize;
    [SerializeField] private string toolTypeRequired;
    
    private GameObject _player; // player reference
    private int _resourceLeft; // this keeps track of how many units is left in the node

    // Saving data
    private string _id;

    private void Start()
    {
        _id = GetComponent<UniqueID>().ID;
        _resourceLeft = resourceNodeSize;
        _player = GameObject.Find("First Person Player"); // gets the player game object
    }

    public void Interact()
    {
        var inventory = _player.GetComponent<PlayerInventoryHolder>(); // gets the inventory of the player

        if (!inventory) return; // if there is no inventory, ignore it
        
        inventory.AddToInventory(resourceItemData, 1);
        _resourceLeft--;
        
        Debug.Log($"Resources Left: {_resourceLeft}");

        if (_resourceLeft <= 0) Destroy(this.gameObject);
    }
}
