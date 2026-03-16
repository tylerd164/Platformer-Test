using System.Runtime.CompilerServices;
using UnityEngine;
using static States;

public class PlayerStateController : MonoBehaviour
{
    // Used for player animations
    private const string SPEED = "Speed";
    private const string JUMPING = "Jumping";
    public PlayerStates currentState { get;private set; }

    //public bool isGrounded { get; set; }
    public bool jumpPressed { get; set; }
    //public bool sprintPressed { get; set; }
    //public bool wallCollision { get; set; }
    public bool isIdle { get; set; }
    public bool interactButtonPressed { get; set; }
    public bool pauseButtonPressed { get; set; }
    public bool exitButtonPressed { get; set; }
    public bool puzzleActive { get; set; }

    //public float currentVeloctiyX { get; set; }

    private Animator animator;
    private PlayerStates lastState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        SetPlayerState(PlayerStates.Idle);

        // player idle, walking, sprint animation
        //animator.SetFloat(SPEED, currentVeloctiyX);
    }
    private void Update()
    {
        EvaluatePlayerState();

        // player idle, walking, sprint animation
        //animator.SetFloat(SPEED, currentVeloctiyX); 
    }

    
    private void EvaluatePlayerState()
    {
        // Jumping has highest priority
        //if (!isGrounded)
       // {
           // SetPlayerState(PlayerStates.Jump);
           // return;
        //}

        // Walking only if there is input AND no wall collision
        //else if ()
       // {
          //  SetPlayerState(PlayerStates.Walking);
           // return;
       // }

        // Idle only if grounded and no other actions
        //else if (isIdle && isGrounded)
       // {
           // SetPlayerState(PlayerStates.Idle);
           // return;
        //}
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
