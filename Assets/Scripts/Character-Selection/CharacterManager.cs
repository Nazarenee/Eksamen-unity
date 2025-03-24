using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    
    public string selectedCharacterPrefab;
    
    private void Awake()
    {
        // Make this a singleton that persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetSelectedCharacter(string characterPrefab)
    {
        selectedCharacterPrefab = characterPrefab;
    }
}