using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public CharacterSelectable[] selectableCharacters;
    public Camera selectionCamera; 
    
    void Start()
    {
        if (selectionCamera == null)
        {
            selectionCamera = Camera.main;
            
            if (selectionCamera == null)
            {
                selectionCamera = FindObjectOfType<Camera>();
                Debug.Log("Found camera: " + (selectionCamera != null));
            }
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionCamera == null)
            {
                Debug.LogError("No camera found for raycasting!");
                return;
            }
            
            Ray ray = selectionCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                
                CharacterSelectable character = hit.collider.GetComponent<CharacterSelectable>();
                
                if (character == null)
                {
                    character = hit.collider.GetComponentInParent<CharacterSelectable>();
                }
                
                if (character != null)
                {
                    Debug.Log("Selected character: " + character.characterName);
                    
                    CharacterManager characterManager = FindObjectOfType<CharacterManager>();
                    if (characterManager == null)
                    {
                        GameObject managerObject = new GameObject("CharacterManager");
                        characterManager = managerObject.AddComponent<CharacterManager>();
                    }
                    
                    characterManager.SetSelectedCharacter(character.characterName);
                    
                    SceneManager.LoadScene("Scene1");
                }
            }
        }
    }
}