using UnityEngine;
using UnityEngine.UIElements;

public class ControlTerminal : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private GameObject popup;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && playerState.interactButtonPressed)
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Object Interacted With!");
        
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
}
