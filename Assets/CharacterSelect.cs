using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public string characterName; // "Hunter", "Mage", or "Warrior"
    
    private void OnMouseDown()
    {
        // Find or create character manager
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();
        if (characterManager == null)
        {
            GameObject managerObject = new GameObject("CharacterManager");
            characterManager = managerObject.AddComponent<CharacterManager>();
        }
        
        // Set selected character
        characterManager.SetSelectedCharacter(characterName);
        
        // Load the main game scene
        SceneManager.LoadScene("Scene1");
    }
}