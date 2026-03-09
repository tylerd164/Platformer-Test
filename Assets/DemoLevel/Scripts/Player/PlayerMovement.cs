using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Movement")]
    [SerializeField] private float playerSpeed = 8f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float airControlMultiplier = 0.6f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private int maxJumps = 1;

    private int jumpsRemaining;
    private Rigidbody2D playerRb;
    private Vector2 lastMoveDirection = Vector2.right;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        HandleWalkInput();
        HandleJump();
        HandleSpriteFlip();

        if (playerState.isGrounded)
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // MOVEMENT

    private void HandleMovement()
    {
        if (!playerState.wallCollision && !playerState.puzzleActive)
        {
            Vector2 input = playerInput.GetMovementVectorNormalized();

            // Sprint modifier
            float speedMultiplier = playerState.sprintPressed ? sprintMultiplier : 1f;

            // Final target speed
            float targetSpeed = input.x * playerSpeed * speedMultiplier;

            float controlMultiplier = playerState.isGrounded ? 1f : airControlMultiplier;

            // Setting x with Player acceleration
            playerState.currentVeloctiyX = Mathf.MoveTowards(playerRb.linearVelocity.x, targetSpeed, acceleration * controlMultiplier * Time.fixedDeltaTime);

            // Applying Player Movement
            playerRb.linearVelocity = new Vector2(playerState.currentVeloctiyX, playerRb.linearVelocity.y);
        }
        else { playerState.currentVeloctiyX = 0f; }
    }

    private void HandleWalkInput()
    {
        if (!playerState.puzzleActive)
        {
            Vector2 input = playerInput.GetMovementVectorNormalized();
            playerState.moveInput = Mathf.Abs(input.x) > 0.01f;
        }
    }

    // JUMPING

    private void HandleJump()
    {
        if (playerState.puzzleActive)
            return;

        if (!playerState.jumpPressed)
            return;

        if (jumpsRemaining <= 0)
            return;

        // Reset vertical velocity for consistent jump height
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);

        jumpsRemaining--;
        playerState.jumpPressed = false; // consume jump input
    }

    // SPRITE FLIP

    private void HandleSpriteFlip()
    {
        if (!playerState.puzzleActive)
        {
            Vector2 input = playerInput.GetMovementVectorNormalized();

            if (Mathf.Abs(input.x) > 0.01f)
                lastMoveDirection = new Vector2(input.x, 0f);

            if (lastMoveDirection.x > 0f)
                playerSprite.flipX = false;

            else if (lastMoveDirection.x < 0f)
                playerSprite.flipX = true;
        }
    }
}