using UnityEngine;
using UnityEngine.InputSystem;
using static States;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string UI = "UI";

    private const string MOVE = "Move";
    private const string JUMP = "Jump";
    private const string SPRINT = "Sprint";
    private const string INTERACT = "Interact";
    private const string PAUSE = "Pause";

    [Header("References")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private PlayerStateController playerState;
    [SerializeField] private GameObject mainMenu;

    [Header("Idle Settings")]
    [SerializeField] private float idleDelay = 2f;

    private InputActionMap playerMap;
    private InputActionMap uiMap;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction interactAction;
    private InputAction pauseAction;

    private Vector2 inputValue;
    private float idleTimer;
    private bool isPaused;

    #region Unity Lifecycle

    private void Awake()
    {
        
        playerMap = inputActions.FindActionMap(PLAYER);
        uiMap = inputActions.FindActionMap(UI);

        moveAction = playerMap.FindAction(MOVE);
        jumpAction = playerMap.FindAction(JUMP);
        sprintAction = playerMap.FindAction(SPRINT);
        interactAction = playerMap.FindAction(INTERACT);
        pauseAction = playerMap.FindAction(PAUSE);
    }

    private void OnEnable()
    {
        playerMap.Enable();
        pauseAction.performed += OnPausePressed;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        playerMap.Disable();
        uiMap.Disable();
    }

    private void Update()
    {
        if (isPaused) return;

        HandleMovementInput();
        HandleIdleState();
    }

    #endregion

    #region Input Handling

    private void HandleMovementInput()
    {
        inputValue = moveAction.ReadValue<Vector2>();

        playerState.jumpPressed = jumpAction.WasPressedThisFrame();
        playerState.interactButtonPressed = interactAction.WasPressedThisFrame();
        playerState.sprintPressed = sprintAction.IsPressed();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    #endregion

    #region Pause Logic

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;

        mainMenu.SetActive(true);

        Time.timeScale = 0f;

        playerMap.Disable();
        uiMap.Enable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        isPaused = false;

        mainMenu.SetActive(false);

        Time.timeScale = 1f;

        uiMap.Disable();
        playerMap.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #endregion

    #region Idle Logic

    private void HandleIdleState()
    {
        if (HasPlayerInput())
        {
            idleTimer = 0f;
            playerState.isIdle = false;
        }
        else
        {
            idleTimer += Time.unscaledDeltaTime;

            if (idleTimer >= idleDelay)
                playerState.isIdle = true;
        }
    }

    private bool HasPlayerInput()
    {
        return
            Mathf.Abs(inputValue.x) > 0.01f ||
            Mathf.Abs(inputValue.y) > 0.01f ||
            jumpAction.WasPressedThisFrame();
    }

    #endregion

    public Vector2 GetMovementVectorNormalized()
    {
        return inputValue.normalized;
    }
}
