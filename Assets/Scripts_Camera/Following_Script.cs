using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);  // Ensure the camera stays behind the scene

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
