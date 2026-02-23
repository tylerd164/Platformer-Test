using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public void PlayGame()
    {
        playerInput.ResumeGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
    }
}
 