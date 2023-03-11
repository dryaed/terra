using System.Collections;
using System.Collections.Generic;
using TMPro;
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
}
