using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopKeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlotUI shopSlotPrefab;
    [SerializeField] private ShoppingCartItemUI shoppingCartItemPrefab;

    [SerializeField] private Button buyTab;
    [SerializeField] private Button sellTab;

    [Header("Shopping Cart")] 
    [SerializeField] private TextMeshProUGUI basketTotalText;
    [SerializeField] private TextMeshProUGUI playerGoldText;
    [SerializeField] private TextMeshProUGUI shopGoldText;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyButtonText;
    
    [Header("Item Preview Section")] 
    [SerializeField] private Image itemPreviewSprite;
    [SerializeField] private TextMeshProUGUI itemPreviewName;
    [SerializeField] private TextMeshProUGUI itemPreviewDescription;

    [SerializeField] private GameObject itemListContentPanel;
    [SerializeField] private GameObject shoppingCartContentPanel;

    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventoryHolder;
    
    private bool _isSelling;

    private Dictionary<InventoryItemData, int> _shoppingCart = new Dictionary<InventoryItemData, int>();
    private Dictionary<InventoryItemData, ShoppingCartItemUI> _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();

    private int basketTotal;

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _shopSystem = shopSystem;
        _playerInventoryHolder = playerInventoryHolder;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        if (buyButton != null)
        {
            buyButtonText.text = _isSelling ? "Sell Items" : "Buy Items";
            buyButton.onClick.RemoveAllListeners();
            if (_isSelling) buyButton.onClick.AddListener(SellItems);
            else buyButton.onClick.AddListener(BuyItems);
        }
        
        
        ClearSlots();

        basketTotalText.enabled = false;
        buyButton.gameObject.SetActive(false);
        basketTotal = 0;
        playerGoldText.text = $"Player Gold {_playerInventoryHolder.PrimaryInventorySystem.Gold}G";
        shopGoldText.text = $"Shop Gold {_shopSystem.AvailableGold}G";
        
        DisplayShopInventory();
        //DisplayPlayerInventory();
    }

    private void BuyItems()
    {
        Debug.Log("Clicked the buy button");
        
        if (_playerInventoryHolder.PrimaryInventorySystem.Gold < basketTotal) return;
        Debug.Log("Passed the check if player has more gold than basket total");

        if (!_playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(_shoppingCart)) return;
        Debug.Log("Passed the check if the player has space in the inventory");

        foreach (var kvp in _shoppingCart)
        {
            _shopSystem.PurchaseItem(kvp.Key, kvp.Value);
            
            for (int i = 0; i < kvp.Value; i++)
            {
                _playerInventoryHolder.PrimaryInventorySystem.AddToInventory(kvp.Key, 1);
            }
        }

        _shopSystem.GainGold(basketTotal);
        _playerInventoryHolder.PrimaryInventorySystem.SpendGold(basketTotal);
        
        RefreshDisplay();
        Debug.Log("Bought Items");
    }

    private void SellItems()
    {
        
    }

    private void DisplayShopInventory()
    {
        foreach (var item in _shopSystem.ShopInventory)
        {
            if (item.ItemData == null) continue;;

            var shopSlot = Instantiate(shopSlotPrefab, itemListContentPanel.transform);
            shopSlot.Init(item, _shopSystem.BuyMarkUp);
        }
    }

    private void DisplayPlayerInventory()
    {
        
    }
    
    private void ClearSlots()
    {
        _shoppingCart = new Dictionary<InventoryItemData, int>();
        _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();
        
        foreach (var item in itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
    private void UpdateItemPreview(ShopSlotUI shopSlotUI)
    {
        
    }

    public void AddItemToCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;

        UpdateItemPreview(shopSlotUI);

        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);
        
        
        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]++;
            var newString = $"{data.DisplayName} ({price}G) x{_shoppingCart[data]}";
            _shoppingCartUI[data].SetItemText(newString);
        }
        else
        {
            _shoppingCart.Add(data, 1);
            var shoppingCartTextObj = Instantiate(shoppingCartItemPrefab, shoppingCartContentPanel.transform);
            var newString = $"{data.DisplayName} ({price}G) x1";
            shoppingCartTextObj.SetItemText(newString);
            _shoppingCartUI.Add(data, shoppingCartTextObj);
        }
        
        basketTotal += price;
        basketTotalText.text = $"Total: {basketTotal}G";

        if (basketTotal > 0 && !basketTotalText.IsActive())
        {
            basketTotalText.enabled = true;
            buyButton.gameObject.SetActive(true);
        }

        CheckCartForAvailableGold();
    }
    
    public void RemoveItemFromCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;

        //UpdateItemPreview(shopSlotUI);

        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);
        
        
        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]--;
            var newString = $"{data.DisplayName} ({price}G) x{_shoppingCart[data]}";
            _shoppingCartUI[data].SetItemText(newString);

            if (_shoppingCart[data] <= 0)
            {
                _shoppingCart.Remove(data);
                var tempObj = _shoppingCartUI[data].gameObject;
                _shoppingCartUI.Remove(data);
                Destroy(tempObj);
            }
        }
        
        basketTotal -= price;
        basketTotalText.text = $"Total: {basketTotal}G";
        
        if (basketTotal <= 0 && basketTotalText.IsActive())
        {
            basketTotalText.enabled = false;
            buyButton.gameObject.SetActive(false);
            ClearItemPreview();
            return;
        }
        
        CheckCartForAvailableGold();
    }

    private void ClearItemPreview()
    {
        
    }


    private void CheckCartForAvailableGold()
    {
        var goldToCheck = _isSelling ? _shopSystem.AvailableGold : _playerInventoryHolder.PrimaryInventorySystem.Gold;
        basketTotalText.color = basketTotal > goldToCheck ? Color.red : Color.white;

        if (_isSelling || _playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(_shoppingCart)) return;

        basketTotalText.text = "Not enough room in inventory";
        basketTotalText.color = Color.red;
    }

    public static int GetModifiedPrice(InventoryItemData data, int amount, float markUp)
    {
        var baseValue = data.GoldValue * amount;

        return Mathf.RoundToInt(baseValue + baseValue * markUp);
    }


    
}
