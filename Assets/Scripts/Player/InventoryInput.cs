using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] public GameObject inventoryUI;

    // Old Input system, Refractor to new 
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame ||
            Gamepad.current?.startButton.wasPressedThisFrame == true)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Time.timeScale = inventoryUI.activeSelf ? 0 : 1;
        }
    }
}

