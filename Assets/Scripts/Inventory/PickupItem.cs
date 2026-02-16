using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
