using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Settings")]
    public Rigidbody2D playerRB; 
    public float timeLimit = 5f;
    public GameObject gameWinPanel;
    
    [Header("State")]
    private bool plate1Pressed;
    private bool plate2Pressed;
    private float timer;

    void Start()
    {
        if (playerRB == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerRB = player.GetComponent<Rigidbody2D>();
        }

        if (playerRB == null)
            Debug.LogError("PuzzleManager: Rigidbody2D missing on Player!");
            
        gameWinPanel.SetActive(false);
    }

    void Update()
    {
        if (timer <= 0) return;

        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            ResetPuzzle();
            Debug.Log("Time's up! Try again.");
        }
    }

    public void PlatePressed(int id)
    {
        if (!plate1Pressed && !plate2Pressed) timer = timeLimit;

        if (id == 1) plate1Pressed = true;
        else if (id == 2) plate2Pressed = true;

        if (plate1Pressed && plate2Pressed)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        timer = 0;
        playerRB.gravityScale = 1f; 
        gameWinPanel.SetActive(true);
        Debug.Log("Puzzle Solved!");
    }

    private void ResetPuzzle()
    {
        plate1Pressed = false;
        plate2Pressed = false;
        timer = 0;
    }
}