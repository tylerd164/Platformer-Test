using System.Collections.Generic;
using UnityEngine;

// Script to be added to game object that will have inventroy (Player)
public class Inventory : MonoBehaviour
{
    [SerializeField] public int size = 20;                                           // Number of inventory slots
    [SerializeField] public List<InventorySlot> slots = new List<InventorySlot>();  // List that stores all InventorySlot objects,
                                                                                    // Each slots holds: Item(InventorySlot), Amount(size)
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private void Awake() // Creates the slots
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < size; i++)         // Creates size number of empty InventorySlots, Adds them to the slot list
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(Item item)
    {
        // check if item is stackable 
        if (item.stackable)                                              // calls item.stackable, if returns true:
        {
            foreach (InventorySlot slot in slots)                       // loop through all slots
            {
                if (slot.item == item && slot.amount < item.maxStack)  // Checks if slot contains the same item, and stack is not full 
                {
                    slot.amount++;
                    audioSource.PlayOneShot(pickupSound);             // If true, Increase the stack count, exit function
                    return true;
                }
            }
        }
                                                                       // If stacking didn't happen, find empty slot
        // check for empty slots 
        foreach (InventorySlot slot in slots)                         // loop through all slots 
        {
            if (slot.IsEmpty())                                      // calls slot.IsEmpty, if returns true: 
            { 
                slot.item = item;                                   // place item in slot, set slot amount to 1, exit function
                slot.amount = 1;
                audioSource.PlayOneShot(pickupSound);
                return true;
            }
        }

        // if inventory full: 
        return false;                                             // Item cannot be added
    }

    public void UseItem(InventorySlot slot, GameObject user)
    {
        if (slot.item != null)
        {
            slot.item.Use(user);
            RemoveItem(slot);
        }
    }
    public void RemoveItem(InventorySlot slot)                  // Removes one item from slot
    {
        slot.amount--;

        if (slot.amount <= 0)                                 // clears slot completely if amount <= 0
        {
            slot.item = null;
            slot.amount = 0;
        }
    }
}