using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private const string DEMOLEVEL = "DemoLevel";

    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private UINavController uiNavController;
    public void DemoLevel()
    {
        SceneManager.LoadScene(DEMOLEVEL);
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
