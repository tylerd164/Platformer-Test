using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableBox2D : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isBeingPushed { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            isBeingPushed = true;
        }
        else
        {
            isBeingPushed = false;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            FindObjectOfType<PlayerLives>().TakeDamage();
        }
    }
}