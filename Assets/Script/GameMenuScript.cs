using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play button clicked!");  // Check if this message appears in the Console
        SceneManager.LoadScene("CharacterSelect"); // Replace with your actual scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}