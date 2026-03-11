using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public GameObject GameWinPanel;
    public string targetTag = "Win"; 

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = moveInput * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            FindObjectOfType<PlayerLives>().TakeDamage();
        }

        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            GameWin();
        }
    }
    
    void GameWin()
    {
        GameWinPanel.SetActive(true);
    }
}
