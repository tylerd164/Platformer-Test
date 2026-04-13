using UnityEngine;

public class ControlTerminal : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ScreenFade screenFade;

    [SerializeField] private GameObject miniGameUI;
    [SerializeField] private GameObject miniGame;

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
                if(!miniGame.activeSelf)
                {
                    Interact();
                    audiomanager.audioInstance.AccessTerminal();
                }
                
            }

            else { Debug.Log("cannot interact"); audiomanager.audioInstance.ScanFail(); }
        }

        if (playerState.puzzleActive && playerState.exitButtonPressed && miniGame.activeSelf)
        {
            ExitMiniGame();
        }
    }

    void Interact()
    {
        StartCoroutine(screenFade.FadeOutRespawn());
        playerInput.MiniGameActive();
        miniGame.SetActive(true);
        miniGameUI.SetActive(true);
        playerState.puzzleActive = true; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            playerInRange = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            playerInRange = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color32(132, 132, 132, 255); // Sets sprite Renderer colour darker (grey);
        }
    }

    public void ExitMiniGame()
    {
        playerInput.MiniGameUnactive();
        playerState.inputBlock = 0.1f;
        StartCoroutine(screenFade.FadeOutRespawn());

        miniGame.SetActive(false);
        miniGameUI.SetActive(false);
        playerState.isPaused = false;
        playerState.puzzleActive = false;
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