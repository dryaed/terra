using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Steamworks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarDisplay : StaticInventoryDisplay
{
    private int _maxIndexSize = 9;
    private int _currentIndex = 0;
    
    private Transform _playerTransform;
    
    [SerializeField] private float _dropOffset = 1f;
    [SerializeField] private float _dropHeight = 1.5f;

    //private PlayerControls _playerControls;
    private void Awake()
    {
        //_playerControls = new PlayerControls();
        if (GameObject.Find("First Person Player").TryGetComponent<Transform>(out _playerTransform)) Debug.Log("Player game object found for HotbarDisplay");
    }

    protected override void Start()
    {
        base.Start();

        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;

        slots[_currentIndex].ToggleHighlight();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }   
    
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    
    // I should probably remake this with a normal input system, but oh well

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetIndex(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetIndex(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetIndex(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetIndex(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetIndex(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SetIndex(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SetIndex(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SetIndex(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SetIndex(8);
        if (Input.GetKeyDown(KeyCode.Minus)) SetIndex(9);
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) UseItem();
        
        if (Input.GetKeyDown(KeyCode.Q)) DropItem();
        
        if (Input.mouseScrollDelta.y >= 1f) ChangeIndex(-1);
        if (Input.mouseScrollDelta.y <= -1f) ChangeIndex(1);
    }

    private void UseItem()
    {
        //implement checks so that you don't use items when using the UI | navigating the inventories
        if (slots[_currentIndex].AssignedInventorySlot.ItemData == null) return;
        slots[_currentIndex].AssignedInventorySlot.ItemData.UseItem();
    }

    private void DropItem()
    {
        if (slots[_currentIndex].AssignedInventorySlot.ItemData == null) return;
        slots[_currentIndex].AssignedInventorySlot.ItemData.DropItem();
        
        if (slots[_currentIndex].AssignedInventorySlot.ItemData.ItemPrefab == null) return;
            
        Instantiate(slots[_currentIndex].AssignedInventorySlot.ItemData.ItemPrefab, _playerTransform.position + _playerTransform.forward * _dropOffset * Random.Range(2f, 4f) - _playerTransform.up * _dropHeight, Quaternion.identity);                
                
        if (slots[_currentIndex].AssignedInventorySlot.StackSize > 1) {
            slots[_currentIndex].AssignedInventorySlot.AddToStack(-1);
            UpdateSlot(slots[_currentIndex].AssignedInventorySlot);
        }
        else
        {
            slots[_currentIndex].AssignedInventorySlot.ClearSlot();
            UpdateSlot(slots[_currentIndex].AssignedInventorySlot);
        }
    }

    private void ChangeIndex(int direction)
    {
        slots[_currentIndex].ToggleHighlight();
        _currentIndex += direction;

        if (_currentIndex > _maxIndexSize) _currentIndex = 0;
        if (_currentIndex < 0) _currentIndex = _maxIndexSize;
        
        slots[_currentIndex].ToggleHighlight();
    }

    private void SetIndex(int newIndex)
    {
        slots[_currentIndex].ToggleHighlight();
        
        if (newIndex > _maxIndexSize) newIndex = 0;
        if (newIndex < 0) newIndex = _maxIndexSize;

        _currentIndex = newIndex;
        
        slots[_currentIndex].ToggleHighlight();
    }
}
