using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private const string MAINMENU = "MainMenuScene";

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject winHelmet;
    [SerializeField] private ControllerFeedBack feedBack;
    [SerializeField] private GameObject firstButton;

    [Header("Vibration Settings - Pick Up")]
    public float intensity = 0.4f;
    public float duration = 0.1f;

    private void Update()
    {
        if (levelCompleteUI.activeSelf)
        {
            // Will defaul to fist button on UI
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audiomanager.audioInstance.ItemPickup();
            StartCoroutine(feedBack.VibrateController(intensity, duration));
            Destroy(winHelmet);

            StartCoroutine(DelayedWinScreen());
        }
    }

    private IEnumerator DelayedWinScreen()
    {
        yield return new WaitForSeconds(duration + 0.1f);

        Time.timeScale = 0f;
        playerInput.PauseGame();
        levelCompleteUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void NextLevel()
    {
        playerInput.ResumeGame(); // resets the pause logic 
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void RestartLevel()
    {
        playerInput.ResumeGame(); // resets the pause logic 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        playerInput.ResumeGame(); // resets the pause logic 
        SceneManager.LoadScene(MAINMENU);
    }
}
