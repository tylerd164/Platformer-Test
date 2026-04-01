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
        pauseAction.performed += OnPausePressed;
        jumpAction.performed += OnJump;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        jumpAction.performed -= OnJump;
        playerMap.Disable();
    }

    private void Update()
    {

        if (playerState.inputBlock > 0f) {playerState.inputBlock -= Time.deltaTime; return; }

        if (pauseMenu.activeSelf)
        {
            // Will defaul to fist button on UI
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
       
        else
        {
            playerState.exitButtonPressed = exitAction.WasPressedThisFrame();
            playerState.interactButtonPressed = interactAction.WasPressedThisFrame();

            HandleMovementInput();
        }
        
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
        if (playerState.isPaused)
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
        if (playerState.isPaused) return;

        playerState.isPaused = true;

        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        playerMap.Disable();
        uiMap.Enable();

        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ResumeGame()
    {
        if (!playerState.isPaused) return;

        playerState.isPaused = false;

        pauseMenu.SetActive(false);
        pausedObject.SetActive(false);

        playerMap.Enable();
        uiMap.Disable();

        Time.timeScale = 1f;
        playerState.inputBlock = 0.1f;
    }

    public void MiniGameActive()
    {
        playerMap.Enable();
        uiMap.Disable();
    }

    public void MiniGameUnactive()
    {
        playerMap.Disable();
        uiMap.Enable();
    }
    #endregion

    public Vector2 GetMovementVectorNormalized()
    {
        return inputValue.normalized;
    }
}
