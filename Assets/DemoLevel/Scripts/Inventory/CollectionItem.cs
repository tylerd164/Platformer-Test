using UnityEngine;

[CreateAssetMenu(menuName = "Items/Collectible")]
public class CollectibleItem : Item
{
    public string collectionID;

    public override void Use(GameObject user)
    {
        Debug.Log("Collected: " + collectionID);
    }
}
