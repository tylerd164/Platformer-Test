using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float force = 2f;

    [Header("PlayerAnimation")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float reducedJumpForce = 10f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float fallLimit = -25f;

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

    [Header("Vibration Settings - Double Jump")]
    public float intensity = 0.8f;
    public float duration = 0.8f;

    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    public ControllerFeedBack feedBack;

    private Vector2 rayDir;
    private Vector2 lastMoveDirection = Vector2.right;

    private int jumpsRemaining;
    private int wallDirection;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isFacingWall;
    private bool givePlayerExtraJump;
    private bool deathTriggered;

    [Header("Player FX")]
    [SerializeField] private PlayerFX playerFX;
    private bool wasGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        playerState.inputBlock = 0.1f;
    }

    private void Update()
    {
        if(!playerState.puzzleActive && playerState.isPlaying)
        {
            CheckSurroundings();
            HandleJumpInput();
            HandleWallSlide();
            ImprovedJumpingPhysics();
            HandleSpriteFlip();
            PlayerAnimation();
        }
        
    }

    private void FixedUpdate()
    {
        if (!playerState.puzzleActive && playerState.isPlaying)
        {
            HandleMovement();
        }

        if (playerState.isDead && !deathTriggered)
        {
            deathTriggered = true;
            playerState.isDead = false;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.up * force;
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
            if (isWallSliding && jumpsRemaining > 0)
            {
                WallJump();
            }
            else if (jumpsRemaining > 0)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        if (!isGrounded)
        {
            StartCoroutine(feedBack.VibrateController(intensity, duration));
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, reducedJumpForce);
            //doublejump particle effect
            playerFX?.PlayDoubleJumpFX();
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //jump particle effect
            playerFX?.PlayJumpFX();
        }


        if (!isGrounded)
        {
            jumpsRemaining--;
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

            // resets player jumps once. Contact with ground resets givePlayerExtraJump. 
            if (!givePlayerExtraJump) return;
            else 
            { 
                jumpsRemaining = maxJumps; 
                givePlayerExtraJump = false;
            }
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

    #region Enviroment Detection

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, levelLayer);
        Vector2 moveInput = playerInput.GetMovementVectorNormalized();

        if(!isWallSliding)
        {
            if (Mathf.Abs(moveInput.x) > 0.01f)
                lastMoveDirection = new Vector2(moveInput.x, 0f);

            if (lastMoveDirection.x > 0f)
            {
                rayDir = Vector2.right;
            }


            else if (lastMoveDirection.x < 0f)
            {
                rayDir = Vector2.left;
            }
        }
       

        RaycastHit2D upperHit = Physics2D.Raycast(upperSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D middleHit = Physics2D.Raycast(middleSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D lowerHit = Physics2D.Raycast(lowerSurfaceCheck.position, rayDir, surfaceCheckDistance, levelLayer);

        // Draw rayCast for debugging
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
        //landing particle effect
        if (isGrounded && !wasGrounded)
        {
            playerFX?.PlayLandingFX();
        }

        wasGrounded = isGrounded;
    }

    private void HandleSpriteFlip()
    {
        Vector2 moveInput = playerInput.GetMovementVectorNormalized();
        Vector2 facingDir = playerSprite.flipX ? Vector2.left : Vector2.right;

        RaycastHit2D facingWallUpper = Physics2D.Raycast(upperSurfaceCheck.position, facingDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D facingWallMiddle = Physics2D.Raycast(middleSurfaceCheck.position, facingDir, surfaceCheckDistance, levelLayer);
        RaycastHit2D facingWalllower = Physics2D.Raycast(lowerSurfaceCheck.position, facingDir, surfaceCheckDistance, levelLayer);
        isFacingWall = facingWallMiddle || facingWalllower || facingWallUpper;

        Debug.DrawRay(
            middleSurfaceCheck.position,
            rayDir * surfaceCheckDistance,
            isFacingWall ? Color.green : Color.red);

        if (Mathf.Abs(moveInput.x) > 0.01f)
            lastMoveDirection = new Vector2(moveInput.x, 0f);

        if (isWallSliding && jumpsRemaining == 0 || !isWallSliding)
        {
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

    #region Character Animation

    private void PlayerAnimation()
    {
        Vector2 vel = rb.linearVelocity;

        playerAnimator.SetBool("isWalking", rb.linearVelocity.x > 0.1f || rb.linearVelocity.x < -0.1f);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetBool("isWallSliding", isWallSliding);
        playerAnimator.SetFloat("isFalling", vel.y);
        playerAnimator.SetFloat("isJumping", vel.y);
    }

    public void PlayerDeathAnimation()
    {
        playerAnimator.SetTrigger("Die");
        playerAnimator.SetBool("isDead", true);
    }
    #endregion

    // Debug - Ground Detection
    void OnDrawGizmosSelected()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Debris")
        {
            this.transform.position = respawnPoint.position;
        }

    }
}
