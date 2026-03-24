using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ControlTerminal : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private PlayerStateController playerState;

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject miniGameUI;
    [SerializeField] private GameObject miniGame;
    [SerializeField] private GameObject miniGameOverUI;
    [SerializeField] private GameObject firstButtonMiniGameOverUI;

    [Header("KeyID")]
    [SerializeField] private string requiredKeyID;
    [SerializeField] private Inventory inventory;

    private bool playerInRange = false;
    private bool canInteract = false;

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

        if (playerState.puzzleActive && playerState.exitButtonPressed && miniGame.activeSelf)
        {
            MiniGameOverUI();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButtonMiniGameOverUI);
        }
    }

    void Interact()
    {
        miniGame.SetActive(true);
        miniGameUI.SetActive(true);
        playerState.puzzleActive = true; 
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

    public void MiniGameOverUI()
    {
        miniGameOverUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButtonMiniGameOverUI);
    }

    public void ExitMiniGame()
    {
        miniGame.SetActive(false);
        miniGameUI.SetActive(false);
        miniGameOverUI.SetActive(false);
        playerState.puzzleActive = false;
    }

    public void ResumeMiniGame()
    {
        miniGameOverUI.SetActive(false);
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