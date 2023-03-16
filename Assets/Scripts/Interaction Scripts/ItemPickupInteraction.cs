using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

[RequireComponent(typeof(UniqueID))]
public class ItemPickupInteraction : Interactable
{
    public InventoryItemData ItemData; // what is the item
    public int stackSize; // how many of the item is there

    [SerializeField] private float _rotationSpeed = 80.0f;
    [SerializeField] private float _bobIntensity = 0.5f;

    private GameObject player;

    private Vector3 pos;

    [SerializeField] private ItemPickupSaveData saveData;
    private string id;
    [SerializeField] private AnimationCurve itemAnimationCurve;

    private void Awake()
    {
        
        saveData = new ItemPickupSaveData(ItemData, stackSize, transform.position, transform.rotation);
        SaveLoad.OnLoadGame += LoadItemData;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().ID;
        if (!SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Add(id, saveData);

        player = GameObject.Find("First Person Player"); // gets the player game object
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        itemAnimationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, _bobIntensity))
        {
            preWrapMode = WrapMode.PingPong,
            postWrapMode = WrapMode.PingPong
        };
    }

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.up);
        transform.position = pos + new Vector3(0, itemAnimationCurve.Evaluate(Time.time % itemAnimationCurve.length), 0);
    }

    public override string GetDescription() // displays next to the crosshair
    {
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
        Destroy(this.gameObject);
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
