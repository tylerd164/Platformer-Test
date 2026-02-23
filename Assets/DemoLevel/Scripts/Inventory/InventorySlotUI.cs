using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] public TextMeshProUGUI amountText;

    public void UpdateSlot(InventorySlot slot)
    {
        if (slot.item == null)
        {
            icon.enabled = false;
            amountText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = slot.item.icon;
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : ""; // explain
        }
    }
}
