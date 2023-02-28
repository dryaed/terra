using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ItemPickupInteraction : Interactable
{
    public InventoryItemData ItemData; // what is the item
    public int stackSize; // how many of the item is there

    public GameObject player;

    [SerializeField] private ItemPickupSaveData saveData;
    private string id;
    private void Awake()
    {
        id = GetComponent<UniqueID>().ID;
        saveData = new ItemPickupSaveData(ItemData, stackSize, transform.position, transform.rotation);
        SaveLoad.OnLoadGame += LoadItemData;
    }
    void Start()
    {
        player = GameObject.Find("First Person Player"); // gets the player game object
        SaveGameManager.data.activeItems.Add(id, saveData);
    }

    public override string GetDescription() // displays next to the crosshair
    {
        //string output = "Press [E] to pick up this item.";
        string output = ItemData.DisplayName.ToString() + " x" + stackSize;
        return output;
    }

    public override void Interact()
    {
        var inventory = player.GetComponent<PlayerInventoryHolder>(); // gets the inventory of the player

        if (!inventory) return; // if there is no inventory, ignore it

        if (inventory.AddToInventory(ItemData, stackSize)) //if it is able to add this item, then ...
        {
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject); // remove the in-world item
        }
    }

    private void LoadItemData(SaveData data)
    {
        
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Remove(id);
        SaveLoad.OnLoadGame -= LoadItemData;
    }
}

[System.Serializable]
public struct ItemPickupSaveData
{
    public InventoryItemData ItemData;
    public int StackSize;
    public Vector3 ItemPosition;
    public Quaternion ItemRotation;

    public ItemPickupSaveData(InventoryItemData _data, int _ssize, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        StackSize = _ssize;
        ItemPosition = _position;
        ItemRotation = _rotation;
    }

}
