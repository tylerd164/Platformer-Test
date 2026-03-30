using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private NewPlayerMovement playerAnimation;
    public static LifeManager Instance;
    public GameObject GameOverUI;
    public bool isPlaying = false;
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
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 1;
            audiomanager.audioInstance.Damage();
        }
        if (collision.gameObject.name == "Obstacle")
        {
            playerHealth -= 1;
            audiomanager.audioInstance.Damage();
            return;
        }
    }

    public void Update()
    {

        Health();

        if (playerHealth <= 0)
        {
            // play death animation
            playerAnimation.PlayerDeathAnimation();
            // have some delay before game over screen. 
            GameOver();
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
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level1");
        //Time.timeScale = 0f;
        GameOverUI.SetActive(true);

    }

}
