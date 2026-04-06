using UnityEngine;

public class StickyScript : MonoBehaviour
{
    private bool isStuck = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck || collision.gameObject == gameObject) return;

        isStuck = true;

        transform.SetParent(collision.transform);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //rb.bodyType = RigidbodyType2D.Kinematic; 
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}