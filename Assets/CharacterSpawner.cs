using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject hunterPrefab;
    public GameObject magePrefab;
    public GameObject warriorPrefab;
    
    void Start()
    {
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "hunter"); // Default to hunter
        GameObject characterToSpawn = null;
        
        switch(selectedCharacter)
        {
            case "hunter":
                characterToSpawn = hunterPrefab;
                Debug.Log("Spawning Hunter");
                break;
                
            case "mage":
                characterToSpawn = magePrefab;
                Debug.Log("Spawning Mage");
                break;
                
            case "warrior":
                characterToSpawn = warriorPrefab;
                Debug.Log("Spawning Warrior");
                break;
        }
        
        if (characterToSpawn != null)
        {
            Instantiate(characterToSpawn, new Vector3(-2.38f, 1.52f, 3.44f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("No character prefab found for: " + selectedCharacter);
        }
    }
}