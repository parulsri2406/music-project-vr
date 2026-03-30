using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move forward/backward
        Vector3 move = transform.forward * verticalInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // Rotate left/right
        Quaternion turn = Quaternion.Euler(0, horizontalInput * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turn);
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}