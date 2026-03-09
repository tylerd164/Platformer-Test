using UnityEngine;

[CreateAssetMenu(menuName = "Items/Key")]
public class KeyItem : Item
{
    [SerializeField] public string keyID;

    public override void Use(GameObject user)
    {
        Debug.Log("Key used: " + keyID);
    }
}
