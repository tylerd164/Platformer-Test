using UnityEngine;

[System.Serializable] 

// Data container, used in Inventory and InventorySlotUI
public class InventorySlot
{
    [SerializeField] public Item item;        // Each slot strores Item, Amount
    [SerializeField] public int amount;

    public bool IsEmpty()                    // Function that returns true/ false, depending if slot has item/ no item
    {
        return item == null;
    }
}

