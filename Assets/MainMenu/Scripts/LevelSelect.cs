using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private const string LEVEL1 = "Level1";
    private const string LEVEL2 = "Level2";

    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private UINavController uiNavController;
    public void Level1()
    {
        SceneManager.LoadScene(LEVEL1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(LEVEL2);
    }

    public void Back()
    {
        levelSelect.SetActive(false);
        mainMenu.SetActive(true);
        uiNavController.RestSelectedButton();
    }
}
