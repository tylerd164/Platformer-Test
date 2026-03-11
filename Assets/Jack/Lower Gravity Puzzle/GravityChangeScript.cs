using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityChangeScript : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float newGravityScale = 1.0f;
    public string targetSceneName = "Lower Gravity Puzzle";

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            ApplyGravityChange();
        }
    }

    void ApplyGravityChange()
    {
        if (rb != null)
        {
            rb.gravityScale = newGravityScale;
            Debug.Log("Gravity modified for: " + SceneManager.GetActiveScene().name);
        }
    }
}
