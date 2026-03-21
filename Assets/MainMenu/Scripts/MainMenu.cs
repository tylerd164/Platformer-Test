using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private UINavController uiNavController;
    public void SelectLevel()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        uiNavController.RestSelectedButton();
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}