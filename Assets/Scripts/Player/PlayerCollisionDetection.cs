using UnityEngine;
public class PlayerCollisionDetection : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;

    [SerializeField] private float wallDistance = 0.3f;
    [SerializeField] private float platformCheckDistance = 0.3f;
    [SerializeField] private Vector2 boxSize = new Vector2 (2, 2);

    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;

    private Vector2 lastMovedirection;
    Vector2 rayCastDirection;

    private void FixedUpdate()
    {
        CheckGroundCollision();
        CheckWallCollision();
    }

    void OnDrawGizmos() // remove after testing 
    {
        Gizmos.color = Color.red;

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down * platformCheckDistance;

        // Draw start box
        Gizmos.DrawWireCube(origin, boxSize);

        // Draw end box
        Gizmos.DrawWireCube(origin + direction, boxSize);

        // Draw lines connecting them
        Gizmos.DrawLine(origin + new Vector3(-boxSize.x / 2, -boxSize.y / 2),
                        origin + direction + new Vector3(-boxSize.x / 2, -boxSize.y / 2));

        Gizmos.DrawLine(origin + new Vector3(boxSize.x / 2, -boxSize.y / 2),
                        origin + direction + new Vector3(boxSize.x / 2, -boxSize.y / 2));
    }


    private void CheckGroundCollision()
    {
        playerState.isGrounded = Physics2D.BoxCast(transform.position,boxSize ,0f, Vector2.down, platformCheckDistance, platformLayer);

        // remove after testing
        

        if (playerState.isGrounded) { Debug.Log("is grounded"); }
        else { Debug.Log("is not grounded"); }
    }

    private void CheckWallCollision()
    {
        //Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        //Vector2 moveDir = new Vector2(inputVector.x, 0f);
        Vector2 direction; //= transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        if (transform.localScale.x > 0) { direction = Vector2.right; }
        else { direction = Vector2.left; }

        playerState.wallCollision = Physics2D.Raycast(transform.position, direction, wallDistance, wallLayer);

        // change raycast direction, inline with direction player is facing
        //if (moveDir.x != 0f) { lastMovedirection = moveDir; }

        //if (lastMovedirection.x > 0f) { rayCastDirection = new Vector2(1, 0); }
        //else if (lastMovedirection.x < 0f) { rayCastDirection = new Vector2(-1, 0); }

        // remove after testing
        Debug.DrawRay(
    transform.position,
    rayCastDirection * wallDistance,
    Color.red);

        //if (playerState.wallCollision)
            //Debug.Log("Wall collision");
    }
}
