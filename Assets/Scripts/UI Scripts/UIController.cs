using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    [SerializeField] private TutorialDisplay tutorialDisplay;
    [SerializeField] private ShopKeeperDisplay shopKeeperDisplay;
    [SerializeField] private DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    private void Awake()
    {
        shopKeeperDisplay.gameObject.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
        tutorialDisplay.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    void Update()
    {
        if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;
        if (tutorialDisplay.gameObject.activeInHierarchy) tutorialDisplay.gameObject.SetActive(false);
        if (shopKeeperDisplay.gameObject.activeInHierarchy) shopKeeperDisplay.gameObject.SetActive(false);
        if (inventoryPanel.gameObject.activeInHierarchy) inventoryPanel.gameObject.SetActive(false);
        if (playerBackpackPanel.gameObject.activeInHierarchy) playerBackpackPanel.gameObject.SetActive(false);
    }
    
    private void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventory)
    {
        shopKeeperDisplay.gameObject.SetActive(true);
        shopKeeperDisplay.DisplayShopWindow(shopSystem, playerInventory);
    }
    
    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
}
