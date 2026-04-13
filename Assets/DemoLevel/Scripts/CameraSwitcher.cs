using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera exteriorCamera;
    public CinemachineCamera interiorCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToOutdoor();
            Debug.Log("out");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToIndoor();
            Debug.Log("indoor");
        }

    }
    public void SwitchToIndoor()
    {
        interiorCamera.Priority = 20;
        exteriorCamera.Priority = 10;
    }

    public void SwitchToOutdoor()
    {
        interiorCamera.Priority = 10;
        exteriorCamera.Priority = 20;
    }
}
