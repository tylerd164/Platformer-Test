using UnityEngine;
using UnityEngine.UIElements;

public class ControlTerminal : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private PlayerStateController playerState;

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject miniGameUI;
    [SerializeField] private GameObject miniGame;

    [Header("KeyID")]
    [SerializeField] private string requiredKeyID;
    [SerializeField] private Inventory inventory;

    private bool playerInRange = false;
    private bool canInteract;

    void Update()
    {
        if (playerInRange && playerState.interactButtonPressed)
        {
            CheckItemID(inventory);

            if (canInteract)
            {
                Interact();
            }
            else { Debug.Log("cannot interact"); }
            
        }
    }

    void Interact()
    {
        miniGame.SetActive(true);
        miniGameUI.SetActive(true);
        playerState.puzzleActive = true;

        if (playerState.exitButtonPressed)
        {
            ExitMiniGame();
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

    public void ExitMiniGame()
    {
        playerState.puzzleActive = false;
        miniGame.SetActive(false);
        miniGameUI.SetActive(false);
    }

    private void CheckItemID(Inventory inventory)
    {
        canInteract = false;

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
        //if (!canInteract)
            //Debug.Log("Key not found");

    }
}