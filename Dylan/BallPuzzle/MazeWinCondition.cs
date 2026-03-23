using UnityEngine;

public class MazeWinCondition : MonoBehaviour
{
    [SerializeField] private ControlTerminal controlTerminal;
    [SerializeField] private Rigidbody2D ballRb;
    [SerializeField] private Transform ballSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ballRb.position = ballSpawnPoint.position;
            controlTerminal.MiniGameOverUI();
        }
    }
}
