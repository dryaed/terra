using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private ShopSlot assignedItemSlot;

    [SerializeField] private Button addItemToCartButton;
    [SerializeField] private Button removeItemFromCartButton;
    
    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp { get; private set; }

    private void Awake()
    {
        itemSprite.preserveAspect = true;
        ClearUISlot();

        addItemToCartButton?.onClick.AddListener(AddItemToCart);
        removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        ParentDisplay = transform.parent.GetComponentInParent<ShopKeeperDisplay>();
    }

    public void Init(ShopSlot slot, float markUp)
    {
        assignedItemSlot = slot;
        MarkUp = markUp;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (assignedItemSlot.ItemData != null) {
            itemSprite.sprite = assignedItemSlot.ItemData.Icon;
            itemSprite.color = Color.white;
            itemName.text = $"{assignedItemSlot.ItemData.DisplayName} - {assignedItemSlot.ItemData.GoldValue}G";
            itemCount.text = $"x{assignedItemSlot.StackSize.ToString()}";
        }
        else ClearUISlot();
    }

    private void ClearUISlot()
    {
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemName.text = "";
        itemCount.text = "";
    }
    
    private void AddItemToCart()
    {
        Debug.Log("Kill Niggers");
    }

    private void RemoveItemFromCart()
    {
        Debug.Log("Roundhouse kick a nigger into concrete");
    }
}
