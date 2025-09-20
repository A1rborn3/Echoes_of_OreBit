using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxFactor = 0.003f; // exact number found through trial and error
    private Material mat;
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - lastCamPos;
        Vector2 offset = mat.mainTextureOffset;
        offset += new Vector2(deltaMovement.x, deltaMovement.y) * parallaxFactor;
        mat.mainTextureOffset = offset;

        lastCamPos = cam.position;
    }
}
