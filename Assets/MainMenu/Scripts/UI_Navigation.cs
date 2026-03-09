using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UINavController : MonoBehaviour
{
    private const string UI = "UI";

    // UI actions 
    private const string NAVIGATE = "Navigate";
    private const string SUBMIT = "Submit";
    private const string CANCEL = "Cancel";

    [SerializeField] private InputActionAsset inputActions;

    [SerializeField] private GameObject firstButton;

    private InputActionMap uiMap;

    private InputAction navigateAction;
    private InputAction submitAction;
    private InputAction cancelAction;

    void Awake()
    {
        uiMap = inputActions.FindActionMap(UI);

        navigateAction = uiMap.FindAction(NAVIGATE);
        submitAction = uiMap.FindAction(SUBMIT);
        cancelAction = uiMap.FindAction(CANCEL);
    }

    void OnEnable()
    {
        uiMap.Enable();
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    void OnDisable()
    {
        uiMap.Disable();
    }

    void Update()
    {
        // If nothing selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}