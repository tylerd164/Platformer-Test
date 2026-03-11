using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int maxLives = 5;
    private static int currentLives = 5;
    public LifeLostFlashScript lifelostflashscript; 
    
    [Header("UI Settings")]
    public Image[] heartSprites; 

    void Start()
    {
        UpdateHeartUI();
    }

    public void TakeDamage()
    {
        currentLives--;

        if (currentLives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    
        if(lifelostflashscript != null) lifelostflashscript.TriggerFlash();

        if (currentLives > 0)
        {
            Invoke("ResetLevel", 1.0f);
        }
        else
        {
            GameOver();
        }
    }

    void UpdateHeartUI()
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            if (i < currentLives)
            {
                heartSprites[i].enabled = true;
            }
            else
            {
                heartSprites[i].enabled = false;
            }
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        currentLives = maxLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}