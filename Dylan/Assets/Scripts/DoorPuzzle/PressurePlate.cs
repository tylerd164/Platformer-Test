using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private const string DOOR_KEY = "DoorKey";
    public GameObject door;

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
        door.SetActive(false);
        Debug.Log("Door Opened");
    }

    void CloseDoor()
    {
        door.SetActive(true);
        Debug.Log("Door Closed");
    }
}