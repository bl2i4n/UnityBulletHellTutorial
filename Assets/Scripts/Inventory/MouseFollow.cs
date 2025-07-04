using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
         
    }
}