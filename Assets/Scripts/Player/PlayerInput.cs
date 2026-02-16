using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using static States;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string MOVE = "Move";
    private const string JUMP = "Jump";
    private const string INTERACT = "Interact";

    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private PlayerStateController playerState;

    [SerializeField] float idleDelay = 2f;

    private Vector2 inputValue;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interact;

    float idleTimer;

    private void OnEnable()
    {
        inputActions.FindActionMap(PLAYER).Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap(PLAYER).Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction(MOVE);
        jumpAction = InputSystem.actions.FindAction(JUMP);
        interact = InputSystem.actions.FindAction(INTERACT);
    }

    private void Update()
    {
        inputValue = moveAction.ReadValue<Vector2>();
        playerState.jumpPressed = jumpAction.WasPressedThisFrame();

        if (HasPlayerInput())
        {
            idleTimer = 0f;
            playerState.isIdle = false;
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDelay)
                playerState.isIdle = true;
        }
    }
    bool HasPlayerInput()
    {
        return
        Mathf.Abs(moveAction.ReadValue<Vector2>().x) > 0.01f || jumpAction.WasPressedThisFrame();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputValue;
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
