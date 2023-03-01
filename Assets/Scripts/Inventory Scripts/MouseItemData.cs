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

    private Transform _playerTransform;

    [SerializeField] private float _dropOffset = 3.0f;
    [SerializeField] private float _dropHeight = 1.0f;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemSprite.preserveAspect = true;
        ItemCount.text = "";

        if (GameObject.Find("First Person Player").TryGetComponent<Transform>(out _playerTransform)) Debug.Log("NIGGAS STOLE DA PLAYER");
    }

    public void UpdateMouseSlot(InventorySlot invSlot) // updates slot ui
    {
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        ItemSprite.sprite = AssignedInventorySlot.ItemData.Icon;
        if (AssignedInventorySlot.StackSize > 1) ItemCount.text = AssignedInventorySlot.StackSize.ToString();
        else ItemCount.text = "";
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, _playerTransform.position + _playerTransform.forward * _dropOffset - _playerTransform.up * _dropHeight, Quaternion.identity);                
                
                ClearUISlot();
                
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
        PointerEventData eventDataCurrentPosition = new(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
