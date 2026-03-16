using UnityEngine;

// Script goes on physical objects in the world
public class PickupItem : MonoBehaviour
{
    [SerializeField] public Item item;      // References the item data (ex: key item)

    private void OnTriggerEnter2D(Collider2D collision)  // runs when something enters the trigger collider 
    {
                                                                       // (should be identified by tag in future (** fix needed **)
        Inventory inventory = collision.GetComponent<Inventory>();    // Tries to get the Inventory component (script) for the object that collided with it
        
        if (inventory != null)
        {
            if (inventory.AddItem(item))                             // Calls the Inventory.AddItem function, if returns true destroy the object
            {
                Destroy(gameObject);
            }
        }
    }
}
