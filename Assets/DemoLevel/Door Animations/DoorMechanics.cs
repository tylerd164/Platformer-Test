using UnityEngine;

public class DoorMechanics : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private PlayerInput playerInput;

    [Header("Door Animation")]
    [SerializeField] Animator animator;


    [Header("KeyID")]
    [SerializeField] private string requiredKeyID;
    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject popup;

    private bool playerInRange = false;
    private bool canInteract = false;

    private void Update()
    {
        if (playerInRange && playerState.interactButtonPressed)
        {
            CheckItemID(inventory);

            if (canInteract)
            {
                canInteract = false;
                animator.SetBool("isOpen", true);
                audiomanager.audioInstance.OpenDoor();
                audiomanager.audioInstance.AccessTerminal();
            }

            else { Debug.Log("cannot interact"); audiomanager.audioInstance.ScanFail(); }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            playerInRange = true;
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            playerInRange = false;
            popup.SetActive(false);
        }
    }
    private void CheckItemID(Inventory inventory)
    {
        if (canInteract) { return; }

        foreach (InventorySlot slot in inventory.slots)
        {
            if (slot.item == null)
                continue; // continue searching slots

            if (slot.item is KeyItem key && key.keyID == requiredKeyID)
            {
                Debug.Log("Correct key found");

                inventory.RemoveItem(slot);
                canInteract = true;

                break; // stop searching slots
            }
        }
        if (!canInteract)
            Debug.Log("Correct Key not found");



    }
}
