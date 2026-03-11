using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatySpaceMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 5f;
    public float maxVelocity = 10f;
    public float floatiness = 0.5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.linearDamping = floatiness;
        rb.angularDamping = 1.0f;
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        float moveUp = 0;

        if(Input.GetKey(KeyCode.Space)) moveUp = 1;
        if(Input.GetKey(KeyCode.LeftControl)) moveUp = -1;

        Vector3 moveDirection = (transform.forward * moveV) + (transform.right * moveH) + (transform.up * moveUp);

        if(rb.linearVelocity.magnitude < maxVelocity)
        {
            rb.AddForce(moveDirection * moveForce, ForceMode.Acceleration);
        }

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.left, mouseY);
     }
}
