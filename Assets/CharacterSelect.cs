using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    // Reference to your character manager
    private CharacterManager characterManager;
    
    void Start()
    {
        // Find the character manager in the scene
        characterManager = FindObjectOfType<CharacterManager>();
        
        // If none exists, create one
        if (characterManager == null)
        {
            GameObject managerObject = new GameObject("CharacterManager");
            characterManager = managerObject.AddComponent<CharacterManager>();
        }
    }
    
    // Call this method from your UI buttons
    public void SelectCharacter(string characterPrefabName)
    {
        // Store the selected character name
        characterManager.SetSelectedCharacter(characterPrefabName);
        
        // Load the main game scene
        SceneManager.LoadScene("Scene1");
    }
}