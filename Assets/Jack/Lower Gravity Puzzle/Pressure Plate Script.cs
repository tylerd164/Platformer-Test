using UnityEngine;

public class GravityPressurePlate : MonoBehaviour
{
    public PuzzleManager manager;
    public int plateID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.PlatePressed(plateID);
        }
    }
}