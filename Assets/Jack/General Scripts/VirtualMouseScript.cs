using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class VirtualMouse : MonoBehaviour
{
    [SerializeField] private GameObject miniGameOverUIPipe;

    [Header("Movement Settings")]
    public float cursorSpeed = 800f;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    [Header("Click Settings")]
    public PlayerStateController playerState;

    private RectTransform _cursorTransform;
    private Image _cursorImage;
    private Canvas _canvas;

    void Start()
    {
        _cursorTransform = GetComponent<RectTransform>();
        _cursorImage = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
        _cursorImage.raycastTarget = false;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!miniGameOverUIPipe.activeSelf)
        {
            HandleMovement();

            if (playerState.interactButtonPressed)
            {
                SimulateClick();
            }
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis(horizontalAxis);
        float y = Input.GetAxis(verticalAxis);
        Vector2 move = new Vector2(x, y);

        _cursorTransform.anchoredPosition += move * cursorSpeed * Time.deltaTime;

        Vector2 canvasSize = _canvas.GetComponent<RectTransform>().sizeDelta;
        float halfWidth = canvasSize.x / 2;
        float halfHeight = canvasSize.y / 2;

        Vector2 clampedPos = _cursorTransform.anchoredPosition;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -halfWidth, halfWidth);
        clampedPos.y = Mathf.Clamp(clampedPos.y, -halfHeight, halfHeight);
        
        _cursorTransform.anchoredPosition = clampedPos;
    }

    void SimulateClick()
{
    Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, _cursorTransform.position);

    PointerEventData pointerData = new PointerEventData(EventSystem.current);
    pointerData.position = screenPos;

    List<RaycastResult> uiResults = new List<RaycastResult>();
    EventSystem.current.RaycastAll(pointerData, uiResults);

    if (uiResults.Count > 0)
    {
        foreach (RaycastResult result in uiResults)
        {
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            return;
        }
    }

    Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
    Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

    RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);

    if (hit.collider != null)
    {
        hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
    }
}
}