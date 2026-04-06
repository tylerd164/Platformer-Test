using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableBox2D : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public bool IsBeingPushed => rb != null && rb.linearVelocity.magnitude > 0.1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            collision.gameObject.GetComponent<PlayerLives>()?.TakeDamage();
        }
    }
    */
}