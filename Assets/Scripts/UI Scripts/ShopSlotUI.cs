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

    private int _tempAmount;
    
    public ShopKeeperDisplay ParentDisplay { get; private set; }
    public float MarkUp { get; private set; }

    public ShopSlot AssignedItemSlot => assignedItemSlot;


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
        _tempAmount = slot.StackSize;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (assignedItemSlot.ItemData != null) {
            itemSprite.sprite = assignedItemSlot.ItemData.Icon;
            itemSprite.color = Color.white;
            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(assignedItemSlot.ItemData, 1, MarkUp);
            itemName.text = $"{assignedItemSlot.ItemData.DisplayName} - {modifiedPrice}G";
            itemCount.text = $"x{assignedItemSlot.StackSize}";
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
        if (_tempAmount <= 0) return;
        
        _tempAmount--;
        ParentDisplay.AddItemToCart(this);
        itemCount.text = $"x{_tempAmount}";
    }

    private void RemoveItemFromCart()
    {
        if (_tempAmount == assignedItemSlot.StackSize) return;
        
        _tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);
        itemCount.text = $"x{_tempAmount}";
    }
}
