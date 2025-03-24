using UnityEngine;

using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject hunterPrefab;
    public GameObject magePrefab;
    public GameObject warriorPrefab;
    
    public Transform spawnPoint;
    
    private Vector3 defaultSpawnPosition = new Vector3(-2.666f, 1.61f, -9.03f);
    
    void Start()
    {
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();
        
        if (characterManager != null)
        {
            string selectedCharacter = characterManager.selectedCharacterPrefab;
            
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
                    characterToSpawn = hunterPrefab;
                    break;
            }
            
            Vector3 spawnPosition = (spawnPoint != null) ? spawnPoint.position : defaultSpawnPosition;
            Instantiate(characterToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}