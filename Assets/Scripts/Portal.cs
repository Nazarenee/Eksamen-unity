using UnityEngine;

public class Portal : MonoBehaviour
{
    private RoomController roomController;
    private LevelLoader levelLoader;


    void Start()
    {
        roomController = FindAnyObjectByType(typeof(RoomController)) as RoomController;
        levelLoader = FindAnyObjectByType(typeof(LevelLoader)) as LevelLoader;

        if (roomController == null)
        {
            Debug.LogError("RoomController not found in the scene!");
        }
        if (levelLoader == null)
        {
            Debug.LogError("LevelLoader not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Hunter") || other.CompareTag("Mage") || other.CompareTag("Warrior") && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("Player entered the portal!");
            levelLoader.LoadNextRoom(); // Calls the LevelLoader's transition method
        }
    }
}