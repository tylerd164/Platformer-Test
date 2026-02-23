using TMPro;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject winHeader;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInput.PauseGame();
            winHeader.SetActive(true);
        }
    }
}
