using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public GameObject virtualCamera;

    //checks if player enters the camera's collision and if so it activates that camera and pans the screen to it
    private void OnTriggerEnter2D(Collider2D other)
    {
    //the player game object must have the "Player" tag or this wont work, && !other.isTrigger makes sure the collider is not a trigger
        if (other.CompareTag("Player") && !other.isTrigger)
        {
        // if both conditions above are true the camera is activated
            virtualCamera.SetActive(true);

        }
    }

//same as above but when player exits camera de-activates
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(false);

        }
    }
}

// https://www.youtube.com/watch?v=yaQlRvHgIvE video i used, follow this to see how to set up the camera triggers in the game world.
// make a gameobject, call it room, give it a Cinemachine virtual camera, give the Room a Polygon collider and match it up with the camera, add "room manager script" and link up the virtual camera with the correct Cinemachine camera
