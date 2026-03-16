using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 12f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Wall Movement")]
    [SerializeField] private float wallSlideSpeed = 1.5f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(16f, 16f);

    [Header("Enviroment Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.right;
    private float coyoteCounter;
    private float jumpBufferCounter;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private int jumpsRemaining;
    private int wallDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!playerState.puzzleActive)
        {
            HandleTimers();
            CheckSurroundings();
            HandleJumpInput();
            HandleWallSlide();
            ImprovedFalling();
            HandleSpriteFlip();
        }
        
    }

    private void FixedUpdate()
    {
        if (!playerState.puzzleActive)
        {
            HandleMovement();
        }

    }

    #region Player Movement

    private void HandleMovement()
    {
        moveInput = playerInput.GetMovementVectorNormalized();
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJumpInput()
    {
        if (playerState.jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }

        if (jumpBufferCounter > 0)
        {
            if (isWallSliding)
            {
                WallJump();
                Debug.Log("wall jump");
                jumpBufferCounter = 0;
            }
            else if (coyoteCounter > 0 || jumpsRemaining > 0)
            {
                Jump();
                Debug.Log("normal jump");
                jumpBufferCounter = 0;
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (!isGrounded)
        {
            jumpsRemaining--;
            coyoteCounter = 0;
        }
    }

    // Player Wall Interaction
    private void HandleWallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
            // if jumps remaining <2 give an extra jump

            rb.linearVelocity = new Vector2(-wallDirection * 2f, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }

        if (isGrounded)
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if(jumpsRemaining > 0)
        {
            float angle = 45f;
            float force = 40f;

            float rad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            rb.AddForce(direction * force, ForceMode2D.Impulse);

            //rb.linearVelocity = new Vector2(-wallDirection * wallJumpForce.x, wallJumpForce.y);
            jumpsRemaining--;

            isWallSliding = false;
        }
    }

    private void ImprovedFalling()
    {
        Vector2 vel = rb.linearVelocity;

        if (vel.y < 0)
        {
            vel += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            vel.y = Mathf.Max(vel.y, -25f);
        }

        rb.linearVelocity = vel;
    }

    #endregion

    #region Timers

    private void HandleTimers()
    {
        // allows jumping just after leaving a ledge and slightly before landing

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }

        else
        {
            coyoteCounter -= Time.deltaTime;
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    #endregion

    #region Enviroment Detection

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        Vector2 rayDir = playerSprite.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, rayDir, wallCheckDistance, wallLayer);

        isTouchingWall = hit;

        if (isTouchingWall)
        {
            wallDirection = (int)Mathf.Sign(hit.normal.x);
        }

        if (isGrounded)
        {
            jumpsRemaining = maxJumps - 1;
        }
    }

    private void HandleSpriteFlip()
    {
        if (!playerState.puzzleActive)
        {
            if (Mathf.Abs(moveInput.x) > 0.01f)
                lastMoveDirection = new Vector2(moveInput.x, 0f);

            if (lastMoveDirection.x > 0f)
            {
                playerSprite.flipX = false;
            }
                

            else if (lastMoveDirection.x < 0f)
            {
                playerSprite.flipX = true;
            }     
        }
    }

    #endregion

    // Debug
    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
