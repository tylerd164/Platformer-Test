using System.Runtime.CompilerServices;
using UnityEngine;
using static States;

public class PlayerStateController : MonoBehaviour
{
    // Used for player animations 
    public PlayerStates currentState { get;private set; }

    public bool isGrounded { get; set; }
    public bool jumpPressed { get; set; }
    public bool walkInput { get; set; }
    public bool wallCollision { get; set; }
    public bool isIdle { get; set; }

    private PlayerStates lastState;

    private void Start()
    {
        SetPlayerState(PlayerStates.Idle);
    }
    private void Update()
    {
        EvaluatePlayerState();
    }

    
    private void EvaluatePlayerState()
    {
        // Jumping has highest priority
        if (jumpPressed)
        {
            SetPlayerState(PlayerStates.Jump);
            return;
        }

        // Walking only if there is input AND no wall collision
        else if (walkInput && isGrounded && !wallCollision)
        {
            SetPlayerState(PlayerStates.Walking);
            return;
        }

        // Idle only if grounded and no other actions
        else if (isIdle && isGrounded)
        {
            SetPlayerState(PlayerStates.Idle);
            return;
        }
    }

    private void SetPlayerState(PlayerStates newState)
    {
        if (newState == lastState)
            return;

        lastState = newState;
        currentState = newState;

        Debug.Log($"STATE CHANGED → {newState}");
    }
}
