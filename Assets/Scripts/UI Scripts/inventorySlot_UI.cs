using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite; // visual item representation
    [SerializeField] private TMP_Text itemCount; // visual stack size representation
    [SerializeField] private InventorySlot assignedInventorySlot; // what actual data slot this represents

    private Button button; // le buttone

    public InventorySlot AssignedInventorySlot => assignedInventorySlot; //getter
    public InventoryDisplay ParentDisplay { get; private set; } // constructor

    private void Awake()
    {
        ClearUISlot(); // clear the slot on creation

        button = GetComponent<Button>(); 
        button?.onClick.AddListener(OnUISlotClick); // adds on click action

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot) // initialize this slot
    {
        assignedInventorySlot = slot; // set itemData for this slot
        UpdateUISlot(slot); // refresh ui for this slot
    }

    public void UpdateUISlot(InventorySlot slot) // refresh ui
    {
        if(slot.ItemData != null) // if the data slot is not empty
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = ""; // if the stack size is 1 don't show number
        }
        else
        {
            ClearUISlot(); // if the data slot is empty clear the ui slot
        }
    }
    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }
    public void ClearUISlot() // clears the ui slot
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this); 
    }
}
