using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public int size = 20;
    [SerializeField] public List<InventorySlot> slots = new List<InventorySlot>();

    // explain all
    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(Item item)
    {
        // check if item is stackable 
        if (item.stackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item && slot.amount < item.maxStack)
                {
                    slot.amount++;
                    return true;
                }
            }
        }

        // check for empty slots 
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.amount = 1;
                return true;
            }
        }

        // if inventory full: 
        return false;
    }

    public void RemoveItem(InventorySlot slot)
    {
        slot.amount--;

        if (slot.amount <= 0)
        {
            slot.item = null;
            slot.amount = 0;
        }
    }
}
