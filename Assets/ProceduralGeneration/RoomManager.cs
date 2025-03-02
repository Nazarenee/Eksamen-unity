namespace ProceduralGeneration
{
    using System.Collections.Generic;
    using UnityEngine;

    public class RoomManager : MonoBehaviour
    {
      private List<GameObject> roomPrefabs;

        public void Start()
        {
            // Saml alle rum fra "Resources/Rooms" mappen
            roomPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms"));


            Instantiate(
                roomPrefabs[0],           // Load det første rum i listen
                new Vector3(0, 0, 0),     // Stedet hvor det bliver spawned på vores scene
                Quaternion.identity);     // Rotationen af rummet (det her er default for objektet)
        }
    
    }
}