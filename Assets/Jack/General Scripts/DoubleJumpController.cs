using UnityEngine;

public class DoubleJumpController : MonoBehaviour
{
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private int jumpCount = 0;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount == 0)
            {
                DoJump();
                jumpCount = 1;
                Debug.Log("Jump 1");
            }
            else if (jumpCount == 1 && Input.GetKey(KeyCode.LeftShift))
            {
                DoJump();
                jumpCount = 2;
                Debug.Log("Jump 2 SUCCESS");
            }
        }
    }

    void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}