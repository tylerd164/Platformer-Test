using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Placed in each UI Slot Prefeb
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] public Image icon;                      // Default Icon
    [SerializeField] public TextMeshProUGUI amountText;     // text to track amout of items in inventory

    public void UpdateSlot(InventorySlot slot)             // Updates the UI to match the data
    {
        if (slot.item == null)                           // If the slot is empyt:
        {
            icon.enabled = false;                       // Hide icon
            amountText.text = "";                      // clear Text
        }
        else                                                                         // If slot has an item: 
        {
            icon.enabled = true;                                                   // Show the icon   
            icon.sprite = slot.item.icon;                                         // Set the sprite from the item (slot.item.icon)
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";     // If slot.amount is >1, show number, else don't show
        }
    }
}
