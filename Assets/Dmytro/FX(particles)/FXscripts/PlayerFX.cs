using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("FX Prefabs")]
    [SerializeField] private GameObject jumpFX;        
    [SerializeField] private GameObject doubleJumpFX;  
    [SerializeField] private GameObject landingFX;     

    [Header("Offsets")]
    [SerializeField] private Vector3 jumpOffset = Vector3.down * 0.2f;
    [SerializeField] private Vector3 landingOffset = Vector3.down * 0.1f;

    public void PlayJumpFX()
    {
        if (jumpFX == null) return;
        Instantiate(jumpFX, transform.position + jumpOffset, Quaternion.identity);
    }

    public void PlayDoubleJumpFX()
    {
        if (doubleJumpFX == null) return;
        Instantiate(doubleJumpFX, transform.position + jumpOffset, Quaternion.identity);
    }

    public void PlayLandingFX()
    {
        if (landingFX == null) return;
        Instantiate(landingFX, transform.position + landingOffset, Quaternion.identity);
    }
}
