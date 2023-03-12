using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ShopInventoryItem> items;
    [SerializeField] private int maxAllowedGold;
    [SerializeField] private float sellMarkUp;
    [SerializeField] private float buyMarkUp;

    public List<ShopInventoryItem> Items => items;
    public int MaxAllowedGold => maxAllowedGold;
    public float SellMarkUp => sellMarkUp;
    public float BuyMarkUp => buyMarkUp;
}

[System.Serializable]
public struct ShopInventoryItem
{
    public InventoryItemData ItemData;
    public int Amount;
}