using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{ 
    public static LifeManager Instance;
    public GameObject GameOverUI;
    public GameObject gameOverCamera;
    public GameObject mainCamera;
    public GameObject Retry;
    public bool isPlaying = false;
    [SerializeField] private PlayerInput playerInput;
    public int playerHealth = 5;

    public GameObject healthUI1;
    public GameObject healthUI2;
    public GameObject healthUI3;
    public GameObject healthUI4;
    public GameObject healthUI5;

    


    private void Awake()
    {
        isPlaying = true;
        Instance = this;
    }

    private void Start()
    {
        GameOverUI.SetActive(false);
        gameOverCamera.SetActive(true);
        Retry.SetActive(false);

        Time.timeScale = 1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 1;

            audiomanager.audioInstance.Damage();
        }
    }

    public void Update()
    {

        Health();

        if (playerHealth <= 0)
        {
            if (isPlaying == true)
            {
                GameOver();

            }
        }
           
        
    }


    public void Health()
    {
        if (playerHealth >= 5)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(true);
            healthUI4.SetActive(true);
            healthUI5.SetActive(true);
        }

        if (playerHealth == 4)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(true);
            healthUI4.SetActive(true);
            healthUI5.SetActive(false);
        }

        if (playerHealth == 3)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(true);
            healthUI4.SetActive(false);
            healthUI5.SetActive(false);
        }

        if (playerHealth == 2)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
            healthUI5.SetActive(false);
        }

        if (playerHealth == 1)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(false);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
            healthUI5.SetActive(false);
        }

        if (playerHealth == 0)
        {
            healthUI1.SetActive(false);
            healthUI2.SetActive(false);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
            healthUI5.SetActive(false);
        }
    }
    public void GameOver()
    {
            isPlaying = false;
            GameOverUI.SetActive(true);
            Retry.SetActive(true);

        mainCamera.SetActive(false);
        gameOverCamera.SetActive(true);

        Time.timeScale = 0;
    }

    public void Button()
    {
        SceneManager.LoadScene("DemoLevel");
    }
}
