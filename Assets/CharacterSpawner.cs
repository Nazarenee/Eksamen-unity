using UnityEngine;

using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    // References to your character prefabs
    public GameObject hunterPrefab;
    public GameObject magePrefab;
    public GameObject warriorPrefab;
    
    // Spawn position (can be overridden in Inspector)
    public Transform spawnPoint;
    
    // Default spawn position values
    private Vector3 defaultSpawnPosition = new Vector3(-2.666f, 1.61f, -9.03f);
    
    void Start()
    {
        // Get the character manager
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();
        
        if (characterManager != null)
        {
            // Get selected character name
            string selectedCharacter = characterManager.selectedCharacterPrefab;
            
            // Spawn the appropriate character
            GameObject characterToSpawn = null;
            
            switch (selectedCharacter)
            {
                case "Hunter":
                    characterToSpawn = hunterPrefab;
                    break;
                case "Mage":
                    characterToSpawn = magePrefab;
                    break;
                case "Warrior":
                    characterToSpawn = warriorPrefab;
                    break;
                default:
                    // Default character if none selected
                    characterToSpawn = hunterPrefab;
                    break;
            }
            
            // Instantiate the character at the spawn point or default position
            Vector3 spawnPosition = (spawnPoint != null) ? spawnPoint.position : defaultSpawnPosition;
            Instantiate(characterToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}