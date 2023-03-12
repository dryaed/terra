using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingCartItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;

    public void SetItemText(string newString)
    {
        itemText.text = newString;
    }
}
