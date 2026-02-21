using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerAcceleration = 20f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 1f;

    private int maxJumps = 1;
    private int jumpsRemaining;

    private Rigidbody2D playerRb;
    private Vector2 lastMovedirection;

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
        PlayerSpriteFilp();
        PlayerWalkInput();
        PlayerJumpFunction();

        if (playerState.isGrounded)
        {
            jumpsRemaining = maxJumps;
            playerState.jumpPressed = false;
        }
        
    }

    private void FixedUpdate()
    {
        PlayerMovementFunction();
    }

    private void PlayerMovementFunction()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, 0f);

        float currentMaxSpeed = playerSpeed;

        //check if player can move, allows player to move in air without activating walk animation
        if (!playerState.wallCollision)
        {
            playerRb.AddForce(Vector2.right * moveDir * playerAcceleration);

            // Mathf.Clamp limits player speed, left and right
            playerRb.linearVelocity = new Vector2(Mathf.Clamp(playerRb.linearVelocity.x, -currentMaxSpeed, currentMaxSpeed), playerRb.linearVelocity.y);
        }
    }
    private void PlayerWalkInput()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        playerState.walkInput = Mathf.Abs(inputVector.x) > 0.01f ;
    }

    public void PlayerJumpFunction()
    {
        if (!playerState.jumpPressed)
            return;

        if (jumpsRemaining == 0)
            return;

        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f); // prevents stacking of forces 
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            Debug.Log("jumped");
            jumpsRemaining--;
        
    }

    // flip player sprite, retaining last move direction
    private void PlayerSpriteFilp()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, 0f);

        if (moveDir.x != 0f) { lastMovedirection = moveDir; }

        if (lastMovedirection.x > 0f ) { playerSprite.flipX = false; }        
        else if (lastMovedirection.x < 0f) { playerSprite.flipX = true; }
    }
}
