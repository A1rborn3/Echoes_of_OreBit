using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    
    public Rigidbody2D targetRb; //ship
    public Movement_Script shipMovement;
    public Camera cam;
    public float baseSize = 20f; //base
    public float maxSizeMultiplier = 2f;
    public float zoomSpeed = 2f;//smooth speed

    void LateUpdate()
    {
        if (targetRb == null || cam == null || shipMovement == null) return;

        // Get current velocity and current maxSpeed
        Vector2 velocity = targetRb.linearVelocity;
        float currentMaxSpeed = shipMovement.maxSpeed;

        // Calculate zoom factor based on velocity relative to current max speed
        float speedFactor = (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y)) / (currentMaxSpeed * 2f);
        speedFactor = Mathf.Clamp01(speedFactor);

        // Calculate target orthographic size
        float targetSize = Mathf.Lerp(baseSize, baseSize * maxSizeMultiplier, speedFactor);

        // Smoothly interpolate camera zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}