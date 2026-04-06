using UnityEngine;

public class ZeroGMovement : MonoBehaviour
{
    public float power = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.WakeUp(); 
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) rb.AddForce(Vector2.up * power);
        if (Input.GetKey(KeyCode.DownArrow)) rb.AddForce(Vector2.down * power);
        if (Input.GetKey(KeyCode.LeftArrow)) rb.AddForce(Vector2.left * power);
        if (Input.GetKey(KeyCode.RightArrow)) rb.AddForce(Vector2.right * power);

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY);

        if(moveDirection.magnitude > 0.1f)
        {
            rb.AddForce(moveDirection * power);
        }
    }
}