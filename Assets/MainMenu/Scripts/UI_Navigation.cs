using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UINavController : MonoBehaviour
{
    private const string UI = "UI";

    [SerializeField] private InputActionAsset inputActions;

    [SerializeField] private GameObject firstButtonMainMenu;
    [SerializeField] private GameObject firstButtonLevelSelect;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject mainMenu;

    private InputActionMap uiMap;

    private void Awake()
    {
        uiMap = inputActions.FindActionMap(UI);
    }

    void OnEnable()
    {
        uiMap.Enable();
        EventSystem.current.SetSelectedGameObject(firstButtonMainMenu);

        if(levelSelect.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstButtonLevelSelect);
        }
        
    }

    private void OnDisable()
    {
        uiMap.Disable();
    }

    private void Update()
    {
        // If nothing selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButtonMainMenu);

            if (levelSelect.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(firstButtonLevelSelect);
            }
        }
    }

    public void RestSelectedButton()
    {
        if(mainMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstButtonMainMenu);
        }

        if(levelSelect.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstButtonLevelSelect);
        }
    }
}