using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TestCollision");
        Inventory inventory = collision.GetComponent<Inventory>();
        

        if (inventory != null)
        {
            if (inventory.AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}
