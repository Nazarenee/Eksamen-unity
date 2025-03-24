using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public CharacterSelectable[] selectableCharacters;
    public Camera selectionCamera; // Reference to your camera
    
    void Start()
    {
        // If camera not assigned in inspector, try to find main camera
        if (selectionCamera == null)
        {
            selectionCamera = Camera.main;
            
            // If still null, try to find any camera
            if (selectionCamera == null)
            {
                selectionCamera = FindObjectOfType<Camera>();
                Debug.Log("Found camera: " + (selectionCamera != null));
            }
        }
    }
    
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if we have a camera
            if (selectionCamera == null)
            {
                Debug.LogError("No camera found for raycasting!");
                return;
            }
            
            Ray ray = selectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                
                // Try to get CharacterSelectable from hit object
                CharacterSelectable character = hit.collider.GetComponent<CharacterSelectable>();
                
                // If direct component not found, try parents
                if (character == null)
                {
                    character = hit.collider.GetComponentInParent<CharacterSelectable>();
                }
                
                // If found, select that character
                if (character != null)
                {
                    Debug.Log("Selected character: " + character.characterName);
                    
                    // Find or create character manager
                    CharacterManager characterManager = FindObjectOfType<CharacterManager>();
                    if (characterManager == null)
                    {
                        GameObject managerObject = new GameObject("CharacterManager");
                        characterManager = managerObject.AddComponent<CharacterManager>();
                    }
                    
                    // Set selected character
                    characterManager.SetSelectedCharacter(character.characterName);
                    
                    // Load the main game scene
                    SceneManager.LoadScene("Scene1");
                }
            }
        }
    }
}