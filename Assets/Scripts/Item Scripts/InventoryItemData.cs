using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
/// <summary>
/// This is a scriptable object that defines items in the game
/// You can inherit from this class to make branched versions of items, e.g. equipment or potions
/// </summary>
public class InventoryItemData : ScriptableObject
{
    public int ID = -1; // Item IDentification number
    public string DisplayName; // Item name for display purposes
    [TextArea(4, 4)]
    public string Description; // Item description for display purposes
    public Sprite Icon; // Item sprite image for display purposes
    public int MaxStackSize; // Item Maximum Stack size for the inventory system
    public GameObject ItemPrefab; // object that is used to spawn the item in world
}
