using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    
    public string targetTag = "Pushable"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
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