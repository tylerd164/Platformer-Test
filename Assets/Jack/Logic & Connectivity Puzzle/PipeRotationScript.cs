using UnityEngine;

public class PipeRotationScript : MonoBehaviour
{
    public float[] correctRotations; // Supports multiple correct angles (e.g., straight pipes)
    [SerializeField] private bool isPlaced = false;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        // Randomize initial rotation
        int rand = Random.Range(0, 4);
        transform.eulerAngles = new Vector3(0, 0, rand * 90);
        
        CheckRotation();
    }

    private void OnMouseDown()
    {
        // Rotate 90 degrees
        transform.Rotate(new Vector3(0, 0, 90));
        CheckRotation();
    }

    private void CheckRotation()
    {
        // Round the Z angle to the nearest whole number to avoid precision errors
        // Use Mathf.Repeat to ensure 360 becomes 0
        float currentZ = Mathf.Round(Mathf.Repeat(transform.eulerAngles.z, 360));

        bool nowCorrect = false;
        foreach (float angle in correctRotations)
        {
            if (Mathf.Abs(currentZ - angle) < 0.1f)
            {
                nowCorrect = true;
                break;
            }
        }

        // Only trigger GameManager if the status actually CHANGED
        if (nowCorrect && !isPlaced)
        {
            isPlaced = true;
            gameManager.correctMove();
        }
        else if (!nowCorrect && isPlaced)
        {
            isPlaced = false;
            gameManager.wrongMove(); // Make sure your GameManager has this!
        }
    }
}