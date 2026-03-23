using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private const string LEVEL1 = "Level1";

    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private UINavController uiNavController;
    public void DemoLevel()
    {
        SceneManager.LoadScene(LEVEL1);
    }

    public void ComingSoon()
    {
        Debug.Log("Coming Soon");
    }

    public void Back()
    {
        levelSelect.SetActive(false);
        mainMenu.SetActive(true);
        uiNavController.RestSelectedButton();
    }
}
