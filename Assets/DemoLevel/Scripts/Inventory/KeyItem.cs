using UnityEngine;

[CreateAssetMenu(menuName = "Items/Key")] // Adds a menu option. (creates Key Item class asset)

public class KeyItem : Item              // inherits from Item class, scriptable Object
{
    [SerializeField] public string keyID;        // Gives an identifiable string, allowing it to be used for certain interactions 

    public override void Use(GameObject user)   //  Replaces the abstract function from Item 
    {
        Debug.Log("Key used: " + keyID);
    }
}
