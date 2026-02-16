using UnityEngine;
public class PlayerCollisionDetection : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;

    [SerializeField] private float wallDistance = 0.3f;
    [SerializeField] private float platformCheckDistance = 0.3f;

    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;

    private Vector2 lastMovedirection;
    Vector2 rayCastDirection;

    private void FixedUpdate()
    {
        CheckGroundCollision();
        CheckWallCollision();
    }

    private void CheckGroundCollision()
    {
        playerState.isGrounded = Physics2D.Raycast(transform.position, Vector2.down, platformCheckDistance, platformLayer);

        // remove after testing
        Debug.DrawRay( 
    transform.position,
    Vector2.down * platformCheckDistance,
    Color.blue);

        //if (playerState.isGrounded) { Debug.Log("is grounded"); }
        //else { Debug.Log("is not grounded"); }
    }

    private void CheckWallCollision()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, 0f);

        playerState.wallCollision = Physics2D.Raycast(transform.position, rayCastDirection, wallDistance, wallLayer);

        // change raycast direction, inline with direction player is facing
        if (moveDir.x != 0f) { lastMovedirection = moveDir; }

        if (lastMovedirection.x > 0f) { rayCastDirection = new Vector2(1, 0); }
        else if (lastMovedirection.x < 0f) { rayCastDirection = new Vector2(-1, 0); }

        // remove after testing
        Debug.DrawRay(
    transform.position,
    rayCastDirection * wallDistance,
    Color.red);

        //if (playerState.wallCollision)
            //Debug.Log("Wall collision");
    }
}
