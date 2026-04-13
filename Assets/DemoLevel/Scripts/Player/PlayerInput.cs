using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        playerMap.Disable();
        uiMap.Disable();
    }

    private void Update()
    {

        if (playerState.inputBlock > 0f) 
        {
            playerState.inputBlock -= Time.deltaTime; 
            return; 
        }

        if (playerState.inputBlock < 0.1f)
        {
            playerState.submitButtonPressed = submitAction.WasPressedThisFrame();
            playerState.jumpPressed = jumpAction.WasPressedThisFrame();
            playerState.exitButtonPressed = cancelAction.WasPressedThisFrame();
            playerState.interactButtonPressed = interactAction.WasPressedThisFrame();

            HandleMovementInput();
        }

        if (pauseMenu.activeSelf)
        {
            // Will defaul to fist button on pause UI
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
    }

    #endregion

    #region Input Handling

    private void HandleMovementInput()
    {
        inputValue = moveAction.ReadValue<Vector2>();
        playerState.jumpPressed = jumpAction.WasPressedThisFrame();

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
            audiomanager.audioInstance.ClickSound();
            pauseMenu.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (playerState.isPaused) return;

        playerState.isPaused = true;

        Time.timeScale = 0f;

        playerMap.Disable();
        uiMap.Enable();
    }

    public void ResumeGame()
    {
        if (!playerState.isPaused) return;

        playerState.isPaused = false;

        pauseMenu.SetActive(false);

        playerMap.Enable();
        uiMap.Disable();

        Time.timeScale = 1f;
        playerState.inputBlock = 0.1f;
    }

    public void MiniGameUnactive()
    {
        playerMap.Enable();
        uiMap.Disable();
    }

    public void MiniGameActive()
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
