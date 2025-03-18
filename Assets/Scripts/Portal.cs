using UnityEngine;

public class Portal : MonoBehaviour
{
    private RoomController roomController; // Reference to RoomController

    void Start()
    {
        // Find the RoomController in the scene
        roomController = FindAnyObjectByType(typeof(RoomController)) as RoomController;

        if (roomController == null)
        {
            Debug.LogError("RoomController not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the portal is the player
        if (other.CompareTag("Hunter"))
        {
            Debug.Log("Player entered the portal!");
            roomController.NextRoom(); // Call the NextRoom() method
        }
    }
}