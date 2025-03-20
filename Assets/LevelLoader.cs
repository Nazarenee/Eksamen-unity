using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; // Assign this in the Inspector
    public float transitionTime = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start"); // Make sure this trigger exists in your Animator

        yield return new WaitForSeconds(transitionTime); // ✅ Corrected: "WaitForSeconds"

        SceneManager.LoadScene(levelIndex);
    }
}