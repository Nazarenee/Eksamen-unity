using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; // Assign the crosshair UI element in Inspector

    void Start()
    {
        Cursor.visible = false; // Hide the default cursor
        Cursor.lockState = CursorLockMode.Confined; // Keep cursor inside the game window
    }

    void Update()
    {
        // Move crosshair to mouse position
        Vector2 mousePosition = Input.mousePosition;
        crosshair.position = mousePosition;
    }
}