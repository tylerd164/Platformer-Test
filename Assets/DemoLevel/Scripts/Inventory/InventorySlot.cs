using UnityEngine;

[System.Serializable] // explain 
public class InventorySlot
{
    [SerializeField] public Item item;
    [SerializeField] public int amount;

    public bool IsEmpty()
    {
        return item == null;
    }
}

