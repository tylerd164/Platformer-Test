using UnityEngine;

// Base Item class for pick-up items
// abstract - cannot be used directly, other classes must inherit from it
// Scriptable Objects - asset that only stores data

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;       // Image that is shown in the inventory UI
    public bool stackable;
    public int maxStack = 1;  // Item Cannot stack more than this

    public abstract void Use(GameObject user);  // Forces every item type to define what happens when it's used
                                                // User parameter is the object using the item (Player Game Object)
}
