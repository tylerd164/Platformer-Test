using UnityEngine;

public class BatMovementTesting : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private GameObject miniGameOverUIMaze;
    [SerializeField] private PlayerStateController playerState;

    private Rigidbody2D ballRb;
    private Vector2 movement;


    private void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }

    //The input direction is updated based on input from the player
    private void Update()
    {
        movement = new Vector2(
        Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        if (!miniGameOverUIMaze.activeSelf)
        ballRb.MovePosition(ballRb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            ballRb.position = ballSpawnPoint.position;
        }
    }
}
