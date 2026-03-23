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
            // when the inventory is active, game time pauses 
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Time.timeScale = inventoryUI.activeSelf ? 0 : 1;
        }
    }
}

