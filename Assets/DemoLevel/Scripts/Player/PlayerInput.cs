using System.Data;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static States;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string UI = "UI";

    // Player movement actions
    private const string MOVE = "Move";
    private const string JUMP = "Jump";
    private const string INTERACT = "Interact";
    private const string PAUSE = "Pause";

    // Exit mini game:
    private const string EXIT = "Exit";

    // UI actions 
    private const string NAVIGATE = "Navigate";
    private const string SUBMIT = "Submit";
    private const string CANCEL = "Cancel";

    [Header("References")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private PlayerStateController playerState;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject pausedObject;

    private InputActionMap playerMap;
    private InputActionMap uiMap;

    // player movement actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction pauseAction;

    // Exit mini game:
    private InputAction exitAction;

    // UI actions
    private InputAction navigateAction;
    private InputAction submitAction;
    private InputAction cancelAction;

    public bool win { get; set; }

    private Vector2 inputValue;
    private float idleTimer;
    private bool isPaused;

    #region Unity Lifecycle

    private void Awake()
    {
        playerMap = inputActions.FindActionMap(PLAYER);
        uiMap = inputActions.FindActionMap(UI);

        // player movement actions
        moveAction = playerMap.FindAction(MOVE);
        jumpAction = playerMap.FindAction(JUMP);
        interactAction = playerMap.FindAction(INTERACT);
        pauseAction = playerMap.FindAction(PAUSE);

        // Exit mini game: 
        exitAction = playerMap.FindAction(EXIT);

        // UI actions
        navigateAction = uiMap.FindAction(NAVIGATE);
        submitAction = uiMap.FindAction(SUBMIT);
        cancelAction = uiMap.FindAction(CANCEL);
    }

    private void OnEnable()
    {
        playerMap.Enable();
        uiMap.Enable();
        pauseAction.performed += OnPausePressed;
        jumpAction.performed += OnJump;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        jumpAction.performed -= OnJump;
        playerMap.Disable();
        uiMap.Disable();
    }

    private void Update()
    { 
        if (isPaused) return;

        // Will defaul to fist button on UI
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        playerState.exitButtonPressed = exitAction.WasPressedThisFrame();
        playerState.interactButtonPressed = interactAction.WasPressedThisFrame();

        HandleMovementInput();
        HandleIdleState();
    }

    #endregion

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) 
        { 
            playerState.jumpPressed = true;

        }
        if (context.canceled) 
        {
            playerState.jumpPressed = false; 
        }
    }

    #region Input Handling

    private void HandleMovementInput()
    {
        inputValue = moveAction.ReadValue<Vector2>();
        playerState.jumpPressed = jumpAction.WasPressedThisFrame();

        //playerState.sprintPressed = sprintAction.IsPressed();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (!playerState.puzzleActive)
            TogglePause();
    }

    #endregion

    #region Pause Logic

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
            pausedObject.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;

        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        playerMap.Disable();
        uiMap.Enable();
        EventSystem.current.SetSelectedGameObject(firstButton);

        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        isPaused = false;

        pauseMenu.SetActive(false);
        pausedObject.SetActive(false);

        uiMap.Disable();
        playerMap.Enable();

        Time.timeScale = 1f;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    #endregion

    #region Idle Logic

    private void HandleIdleState()
    {
        if (HasPlayerInput())
        {
            playerState.isIdle = false;
        }
        else
        {
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
