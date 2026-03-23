using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    //Stores the input direction and rigidbody component and speed of the bat
    Vector2 inputDirection;
    Rigidbody2D rb;
    public float speed = 500f;
    private AudioSource audioSource;
    public AudioClip collisionSound;
    Vector2 spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets the Rigidbody2D component attached to the bat
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = new Vector2(-8, -4);
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
        rb.AddForce(inputDirection * speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall_0")
        {
            inputDirection = Vector2.zero;
            this.transform.position = spawnPosition;
            audioSource.PlayOneShot(collisionSound);
        }

        if (collision.gameObject.name == "ConductorStorage")
        { SceneManager.LoadScene(""); }
    }
}
