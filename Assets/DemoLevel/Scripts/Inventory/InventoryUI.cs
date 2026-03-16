using UnityEngine;

// Inventory UI Manager, Place on Game Oject
public class InventoryUI : MonoBehaviour
{
    [SerializeField] public Inventory inventory;                  // Add the Inventory component (oject with the Inventory script)
    [SerializeField] public InventorySlotUI[] slotsUI;           // Array of InventroySlotsUI, add in inspector

    private void Update()                                       // Updates the inventory UI. Updating every frame is unecessary, fine for now. 
    {
        for (int i = 0; i < slotsUI.Length; i++)               // Loops through every slotsUI 
        {
            slotsUI[i].UpdateSlot(inventory.slots[i]);        // Sends the inventory data to InventorySlotUI 
        }
    }
}
