using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionSystem : MonoBehaviour
{
    public GameObject hunterParent;
    public GameObject mageParent;
    public GameObject warriorParent;
    
    private bool selectionMade = false;
    
    void Update()
    {
        // Only allow selection if one hasn't been made yet
        if (selectionMade)
            return;
            
        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == hunterParent || hit.collider.gameObject.transform.IsChildOf(hunterParent.transform))
                {
                    Debug.Log("You selected Hunter");
                    PlayerPrefs.SetString("SelectedCharacter", "hunter");
                    selectionMade = true;
                    LoadGameScene();
                }
                else if (hit.collider.gameObject == mageParent || hit.collider.gameObject.transform.IsChildOf(mageParent.transform))
                {
                    Debug.Log("You selected Mage");
                    PlayerPrefs.SetString("SelectedCharacter", "mage");
                    selectionMade = true;
                    LoadGameScene();
                }
                else if (hit.collider.gameObject == warriorParent || hit.collider.gameObject.transform.IsChildOf(warriorParent.transform))
                {
                    Debug.Log("You selected Warrior");
                    PlayerPrefs.SetString("SelectedCharacter", "warrior");
                    selectionMade = true;
                    LoadGameScene();
                }
                else
                {
                    // This will help you see what you're actually hitting
                    Debug.Log("You clicked on: " + hit.collider.gameObject.name);
                }
            }
        }
    }
    
    private void LoadGameScene()
    {
        Debug.Log("Loading Scene1");
        SceneManager.LoadScene("Scene1");
    }
}