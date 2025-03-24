using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; 

    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; // Fully lock the cursor to the center
    }

    void Update()
    {
        // Keep crosshair at the center of the screen
        if (crosshair != null)
        {
            crosshair.position = new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }
}