using UnityEngine;

public class Movement_Script : MonoBehaviour
{
    public float thrustForce = 5f;
    public float rotationSpeed = 180f;
    public float slowDownRate = 5f;  //slow down velocity
    public float slowDownRotationRate = 180f;  //slow down rotation
    public float AmbiantslowDownRate = 0f;  //if we want to slow the player down with no inputs
    public float AmbiantslowDownRotationRate = 0f;
    public float maxSpeed = 10f;


    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        // Rotate the ship
        float rotationInput = -Input.GetAxis("Horizontal"); // A = -1, D = 1
        transform.Rotate(0, 0, rotationInput * rotationSpeed * Time.deltaTime);

        // Gradually slow velocity to zero
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, AmbiantslowDownRate * Time.fixedDeltaTime);



        // Gradually slow angular velocity to zero
        rb.angularVelocity = Mathf.MoveTowards(rb.angularVelocity, 0f, AmbiantslowDownRotationRate * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {

            // Normal thrust input
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

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        
    }
}
