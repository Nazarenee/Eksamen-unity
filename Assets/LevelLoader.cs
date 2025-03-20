using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; // Assign in the Inspector
    public float transitionTime = 1f; 
    private RoomController roomController;

    void Start()
    {
        roomController = FindAnyObjectByType(typeof(RoomController)) as RoomController;
        if (roomController == null)
        {
            Debug.LogError("RoomController not found in the scene!");
        }
    }

    public void LoadNextRoom()
    {
        StartCoroutine(TransitionRoom());
    }

    IEnumerator TransitionRoom()
    {
        Debug.Log("Fading out..."); 
        transition.SetTrigger("Start"); // Trigger fade-out

        yield return new WaitForSeconds(transitionTime); // Wait for fade-out to complete

        roomController.NextRoom(); // Now switch rooms

        yield return new WaitForSeconds(0.1f); // Small delay for smoothness

        Debug.Log("Fading in...");
        transition.SetTrigger("End"); // Trigger fade-in
    }
}