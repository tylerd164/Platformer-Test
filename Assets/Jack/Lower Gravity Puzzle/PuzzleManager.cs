using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Rigidbody2D playerRB; 
    public float timeLimit = 5f;
    public GameObject GameWinPanel;
    
    private bool plate1Pressed = false;
    private bool plate2Pressed = false;
    private float timer = 0f;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ResetPuzzle();
                Debug.Log("Time's up! Try again.");
            }
        }
    }

    public void PlatePressed(int id)
    {
        if (id == 1) plate1Pressed = true;
        if (id == 2) plate2Pressed = true;

        if (timer <= 0) timer = timeLimit;

        if (plate1Pressed && plate2Pressed)
        {
            playerRB.gravityScale = 0.5f;
            timer = 0;
            Debug.Log("Puzzle Solved!");
            GameWinPanel.SetActive(true);
        }
    }

    void ResetPuzzle()
    {
        plate1Pressed = false;
        plate2Pressed = false;
        timer = 0;
    }
}