using UnityEngine;

public class ControlsTutorial : MonoBehaviour
{
    private const string PLAYER = "Player";
    [SerializeField] private GameObject doubleJumpTutorial;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            doubleJumpTutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER))
        {
            doubleJumpTutorial.SetActive(false);
        }
    }
}
