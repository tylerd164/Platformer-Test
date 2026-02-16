using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")] // needs explaining 
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public int maxStack = 1;
}
