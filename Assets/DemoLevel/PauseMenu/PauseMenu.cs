using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private const string MAINMENU = "MainMenuScene";

    [SerializeField] private PlayerInput playerInput;

    public void PlayGame()
    {
        playerInput.ResumeGame();
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void ExitGame()
    {
        playerInput.ResumeGame(); // resets the pause logic 
        SceneManager.LoadScene(MAINMENU);
    }

}
 