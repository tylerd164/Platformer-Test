using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScriptNewIPS : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int maxJump = 2;
    int jumpsRemaining;
    
    private Rigidbody2D rb;
    private InputSystem_Actions actions;
    private bool isTouchingSomething;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        actions = new InputSystem_Actions();
    }

    void OnEnable() => actions.Player.Enable();
    void OnDisable() => actions.Player.Disable();

    void Update()
    {
        float moveInput = actions.Player.Move.ReadValue<Vector2>().x;
        rb.linearVelocityX = moveInput * speed;

        if (actions.Player.Jump.triggered && isTouchingSomething)
        {
            rb.linearVelocityY = jumpForce;
        }
        if (jumpsRemaining > 0)
        {
            jumpsRemaining--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingSomething = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingSomething = false;
        jumpsRemaining = maxJump;
    }
}