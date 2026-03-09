using UnityEngine;
public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public int maxStack = 1;

    public abstract void Use(GameObject user);
}
