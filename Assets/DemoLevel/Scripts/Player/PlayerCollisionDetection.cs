using System.Xml.Linq;
using UnityEngine;
public class PlayerCollisionDetection : MonoBehaviour
{
    private const string DOOR_KEY = "DoorKey";
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController playerState;

    [Header("Collision Values")]
    [SerializeField] private float wallCheckDistance = 5;
    [SerializeField] private float platformCheckDistance = 5;
    [SerializeField] private Vector2 boxSize = new Vector2(2, 2);

    [Header("Collision Check Points")]
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private Transform platformCheckPoint;

    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;

    Vector2 lastMoveDir;
    Vector2 direction;
    private void FixedUpdate()
    {
        CheckGroundCollision();
        CheckWallCollision();
    }

    void OnDrawGizmos() // remove after testing 
    {
        Gizmos.color = Color.red;

        Vector3 origin = platformCheckPoint.position;
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
        playerState.isGrounded = Physics2D.BoxCast(platformCheckPoint.position, boxSize, 0f, Vector2.down, platformCheckDistance, platformLayer);

        // remove after testing


        //if (playerState.isGrounded) { Debug.Log("is grounded"); }
        //else { Debug.Log("is not grounded"); }
    }

    private void CheckWallCollision()
    {

        Vector2 input = playerInput.GetMovementVectorNormalized();

        if (Mathf.Abs(input.x) > 0.01f)
            lastMoveDir = new Vector2(input.x, 0f);

        if (lastMoveDir.x > 0f)
            direction = Vector2.right;

        else if (lastMoveDir.x < 0f)
            direction = Vector2.left;

        playerState.wallCollision = Physics2D.Raycast(wallCheckPoint.position, direction, wallCheckDistance, wallLayer);

        Debug.DrawRay(wallCheckPoint.position, direction * wallCheckDistance, Color.red);

        if (playerState.wallCollision)
            Debug.Log("Wall collision");
    }
}
