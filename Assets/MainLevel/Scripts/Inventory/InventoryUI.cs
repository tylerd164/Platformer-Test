using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] public InventorySlotUI[] slotsUI;

    private void Update()
    {
        for (int i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].UpdateSlot(inventory.slots[i]); // explain
        }
    }
}
