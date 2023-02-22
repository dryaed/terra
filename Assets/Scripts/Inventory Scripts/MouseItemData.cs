using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot) // updates slot ui
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        if (invSlot.StackSize > 1) ItemCount.text = invSlot.StackSize.ToString();
        else ItemCount.text = "";
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        // ?TODO: controller support
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearUISlot();
                // TODO: drop the item into the world
            }
        }
    }

    public void ClearUISlot() // clear the ui for this slot
    {
        AssignedInventorySlot.ClearSlot(); 
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
        ItemCount.text = "";
    }

    public static bool IsPointerOverUIObject() // checks if the mouse pointer is above a ui object
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
