[System.Serializable] // explain 
public class InventorySlot
{
    public Item item;
    public int amount;

    public bool IsEmpty()
    {
        return item == null;
    }
}

