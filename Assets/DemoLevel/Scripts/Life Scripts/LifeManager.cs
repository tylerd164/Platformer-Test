using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private NewPlayerMovement playerAnimation;
    [SerializeField] private GameObject firstButtonGameOver;

    public static LifeManager Instance;
    public GameObject gameOverUI;
    public int playerHealth = 4;
    public AudioClip damageSound;
    private AudioSource audioSource;

    public GameObject healthUI1;
    public GameObject healthUI2;
    public GameObject healthUI3;
    public GameObject healthUI4;

    [Header("Damage / Invulnerability")]
    [SerializeField, Tooltip("Seconds after taking damage during which further damage is ignored.")] 
    private float damageCooldown = 0.5f;
    private float lastDamageTime = -Mathf.Infinity;

    private float deathTimer = 0f;
    private bool selected = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    private void Start()
    {
        playerState.isPlaying = true;
        playerState.isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore further damage if still within the invulnerability window
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 1;
            //audiomanager.audioInstance.Damage();
            audioSource.PlayOneShot(damageSound);
            lastDamageTime = Time.time;
        }
        else if (collision.gameObject.name == "Debris")
        {
            playerHealth -= 1;
            //audiomanager.audioInstance.Damage();
            audioSource.PlayOneShot(damageSound);
            lastDamageTime = Time.time;
            return;
        }
    }

    public void Update()
    {

        Health();

        if (playerHealth <= 0)
        {
            playerState.isDead = true;
            Death();
        }

        if (playerState.isDead)
        {
            deathTimer += Time.deltaTime;

            if (deathTimer >= 4f)
            {
                playerState.isDead = false;
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);

                if (!selected)
                {
                    selected = true;
                    EventSystem.current.SetSelectedGameObject(firstButtonGameOver);
                }   
            }
        }
    }

    public void Health()
    {
        if (playerHealth >= 4)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(true);
            healthUI4.SetActive(true);
        }

        if (playerHealth == 3)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(true);
            healthUI4.SetActive(false);
        }

        if (playerHealth == 2)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(true);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
        }

        if (playerHealth == 1)
        {
            healthUI1.SetActive(true);
            healthUI2.SetActive(false);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
        }

        if (playerHealth == 0)
        {
            healthUI1.SetActive(false);
            healthUI2.SetActive(false);
            healthUI3.SetActive(false);
            healthUI4.SetActive(false);
        }
    }
    private void Death()
    {
        playerState.isPlaying = false;
        playerAnimation.PlayerDeathAnimation();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
