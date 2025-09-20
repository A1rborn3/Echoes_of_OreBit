using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMovementMap : MonoBehaviour
{

    private Camera cam;
    public float zoomSpeed = 10f; 
    public float minZoom = 2f;
    public float maxZoom = 50;
    public float panSpeed = 1f; // drag speed
    private Vector3 dragOrigin;
    Spawn_Galaxy script;

    void Start()
    {
        cam = GetComponent<Camera>();

        GameObject starMapGenerator = GameObject.Find("StarMapGenerator");
        if (starMapGenerator != null)
        {
            script = starMapGenerator.GetComponent<Spawn_Galaxy>();
            if (script != null)
            {
                maxZoom = (script.ringCount + 1) * 10f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float maxRadius = script.baseRingSpacing * script.ringCount;
        HandleZoom();
        HandlePan();
    }


    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.0001f)
        {
            // Get mouse position before zoom
            Vector3 mouseWorldBefore = cam.ScreenToWorldPoint(Input.mousePosition);

           //zooming
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
           
            // Get mouse position after zoom
            Vector3 mouseWorldAfter = cam.ScreenToWorldPoint(Input.mousePosition);

            // Offset camera so zoom focuses on mouse
            transform.position += mouseWorldBefore - mouseWorldAfter;
        }
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference * panSpeed;

            // checking borders before movement.the border is based on how many rings are visable
            float maxRadius = script.baseRingSpacing * script.ringCount;
            newPosition.x = Mathf.Clamp(newPosition.x, -maxRadius, maxRadius);
            newPosition.y = Mathf.Clamp(newPosition.y, -maxRadius, maxRadius);

            transform.position = newPosition;
        }
    }

}
