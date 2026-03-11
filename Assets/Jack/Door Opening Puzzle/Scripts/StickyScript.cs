using UnityEngine;

public class StickyScript : MonoBehaviour
{
    private bool isStuck = false;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (isStuck || collision.gameObject == gameObject) return;

        isStuck = true;

        
        transform.SetParent(collision.transform);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}