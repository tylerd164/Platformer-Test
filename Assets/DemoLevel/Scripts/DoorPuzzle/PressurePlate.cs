using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private const string DOOR_KEY = "DoorKey";
    [SerializeField] Animator pressurePlateDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(DOOR_KEY))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(DOOR_KEY))
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        pressurePlateDoor.SetBool("isOpen", true);
        audiomanager.audioInstance.OpenDoor();
        Debug.Log("Door Opened");
    }

    void CloseDoor()
    {
        
        Debug.Log("Door Closed");
    }
}