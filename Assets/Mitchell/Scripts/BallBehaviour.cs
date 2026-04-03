using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    //Stores the input direction and rigidbody component and speed of the bat
    Vector2 inputDirection;
    Rigidbody2D rb;
    public float speed = 500f;
    private AudioSource audioSource;
    public AudioClip collisionSound;
    public Transform spawnPosition;

    [SerializeField] private ControlTerminal controlTerminal;
    [SerializeField] private GameObject miniGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets the Rigidbody2D component attached to the bat
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    //The input direction is updated based on input from the player
    void Update()
    {
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //Force is added to the bat in the direction of the players input
    void FixedUpdate()
    {
        if (miniGame.activeSelf)
        {
            rb.AddForce(inputDirection * speed * Time.deltaTime);
            rb.linearVelocity = new Vector2(inputDirection.x *  5, inputDirection.y * 5);   
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MazeWall"))
        {
            this.transform.position = spawnPosition.position;
            rb.linearVelocity = Vector2.zero;
            audioSource.PlayOneShot(collisionSound);
        }

        if (other.CompareTag("ConductorStorage"))
        {
            this.transform.position = spawnPosition.position;
            rb.linearVelocity = Vector2.zero;
            controlTerminal.ExitMiniGame();
        }
    }
}
