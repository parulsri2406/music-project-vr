using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        
    }
   void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ball"))
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }
}
}