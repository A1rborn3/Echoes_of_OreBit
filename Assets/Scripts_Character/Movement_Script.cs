using UnityEngine;

public class Movement_Script : MonoBehaviour
{
    public float thrustForce = 5f;
    public float rotationSpeed = 180f;
    public float slowDownRate = 5f;  // How quickly to slow down velocity
    public float slowDownRotationRate = 180f;  // How quickly to slow down rotation


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the ship
        float rotationInput = -Input.GetAxis("Horizontal"); // A = -1, D = 1
        transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {

            // Normal thrust input when space NOT pressed
            float thrustInput = Input.GetAxis("Vertical"); // W = 1, S = -1
        if (thrustInput < 0)
        {
            // Gradually slow velocity to zero
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, slowDownRate * Time.fixedDeltaTime);

            // Gradually slow angular velocity to zero
            rb.angularVelocity = Mathf.MoveTowards(rb.angularVelocity, 0f, slowDownRotationRate * Time.fixedDeltaTime);
        }
        else
        {
            Vector2 thrust = transform.up * thrustInput * thrustForce;
            rb.AddForce(thrust);
        }
        
    }
}
