using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;
    private Camera cam;
    private Vector2 startPos;
    // a property which is similar to a variable but is a function but changes everytime we use it
                            // cast to vector 2 or change the type to a vector 2 since we don't need the z value
    private Vector2 travel => (Vector2)cam.transform.position - startPos;
    
    private void Awake()
    {
        // gets main camera
        cam = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}
