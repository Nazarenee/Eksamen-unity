using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; 

    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined; // Keep cursor inside the game window
    }

    void Update()
    {
        // Move crosshair to mouse position
        Vector2 mousePosition = Input.mousePosition;
        crosshair.position = mousePosition;
    }
}