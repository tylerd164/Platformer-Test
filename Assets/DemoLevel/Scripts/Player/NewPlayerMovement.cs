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
    [SerializeField] private float reducedJumpForce = 10f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float fallLimit = -25f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Wall Movement")]
    [SerializeField] private float wallSlideSpeed = 1.5f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(16f, 16f);

    [Header("Enviroment Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform upperSurfaceCheck;
    [SerializeField] private Transform middleSurfaceCheck;
    [SerializeField] private Transform lowerSurfaceCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private float surfaceCheckDistance = 0.5f;
    [SerializeField] private LayerMask levelLayer;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private Vector2 rayDir;
    private Vector2 lastMoveDirection = Vector2.right;

    private float coyoteCounter;
    private float jumpBufferCounter;
    private int jumpsRemaining;
    private int wallDirection;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isFacingWall;
    private bool givePlayerExtraJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(!playerState.puzzleActive)
        {
            HandleTimers();
            CheckSurroundings();
            HandleJumpInput();
            HandleWallSlide();
            ImprovedJumpingPhysics();
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
        if (isTouchingWall && jumpsRemaining < 1 && !isFacingWall)
        {
            Movement();
        }

        if (!isTouchingWall)
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector2 moveInput = playerInput.GetMovementVectorNormalized();
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
            if (isWallSliding && jumpsRemaining > 0)
            {
                WallJump();
                Debug.Log("wall jump");
                jumpBufferCounter = 0;
            }
            else if (coyoteCounter > 0 || jumpsRemaining > 0)
            {
                Jump();
                jumpBufferCounter = 0;
            }
        }
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, reducedJumpForce);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


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

            // sliding mechanic: 
            rb.linearVelocity = new Vector2(-wallDirection * 2f, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));

            if (!givePlayerExtraJump) return;
            else { jumpsRemaining = maxJumps; givePlayerExtraJump = false; }
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        rb.linearVelocity = new Vector2(-wallDirection * wallJumpForce.x, wallJumpForce.y);

        if (!isGrounded)
        {
            jumpsRemaining--;
            coyoteCounter = 0;
        }
    }

    private void ImprovedJumpingPhysics()
    {
        Vector2 vel = rb.linearVelocity;

        // Improves falling Physics, adding upwards force when falling:
        if (vel.y < 0)
        {
            vel += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            vel.y = Mathf.Max(vel.y, fallLimit);
        }

        rb.linearVelocity = vel;
    }

    #endregion

    #region Timers

    private void HandleTimers()
    {
        // allows jumping just after leaving a ledge
        // player can fall off a ledge and will be able to jump 1x 

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, levelLayer);

        if(!isWallSliding)
        {
            rayDir = playerSprite.flipX ? Vector2.left : Vector2.right;
        }

        RaycastHit2D upperHit = Physics2D.Raycast(upperSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D middleHit = Physics2D.Raycast(middleSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D lowerHit = Physics2D.Raycast(lowerSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);

        // Draw rayCast
        Debug.DrawRay(
            upperSurfaceCheck.position,
            rayDir * surfaceCheckDistance,
            upperHit ? Color.green : Color.red);
        Debug.DrawRay(
            middleSurfaceCheck.position,
            rayDir * surfaceCheckDistance,
            middleHit ? Color.green : Color.red);
        Debug.DrawRay(
            lowerSurfaceCheck.position,
            rayDir * surfaceCheckDistance,
            lowerHit ? Color.green : Color.red);

        isTouchingWall = upperHit || middleHit || lowerHit;

        if (isGrounded)
        {
            jumpsRemaining = maxJumps - 1;
            givePlayerExtraJump = true;
        }
    }

    private void HandleSpriteFlip()
    {
        Vector2 moveInput = playerInput.GetMovementVectorNormalized();
        Vector2 facingDir = playerSprite.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D facingWall = Physics2D.Raycast(middleSurfaceCheck.position, facingDir, surfaceCheckDistance, levelLayer);
        isFacingWall = facingWall;

        Debug.DrawRay(
            middleSurfaceCheck.position,
            rayDir * surfaceCheckDistance,
            facingWall ? Color.green : Color.red);

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

    #endregion

    // Debug
    void OnDrawGizmosSelected()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
