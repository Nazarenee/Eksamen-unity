namespace ProceduralGeneration
{
    using System.Collections.Generic;
    using UnityEngine;

    public class RoomManager : MonoBehaviour
    {
        private GameObject playerPrefab;
        private List<GameObject> enemyPrefabs;
        private List<GameObject> roomPrefabs;

        public void Start()
        {
            // Saml alle rum fra "Resources/Rooms" mappen
            roomPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms"));


            // Placeholder for at prøve at spawne et rum
            Instantiate(
                roomPrefabs[0],           // Load det første rum i listen
                new Vector3(0, 0, 0),     // Stedet hvor det bliver spawned på vores scene
                Quaternion.identity);     // Rotationen af rummet (det her er default for objektet)
        }
    }

    //TODO
    /*  1
     * Ved trigger af nyt rum, tjek bool for om shop allerede har været spawned, hvis ikke så roll dice om spawn.
     */

    /*  2
     * Find ud af om vi skal have en liste over hvilke rum der er spawned, og hvilke der ikke er.
     */

    /*  3
     * Slet forrige rum vi har været i, når vi enter portalen.
     */


    /*  4
     * Random generate crushable objects som der kan indeholde loot / currency?
     */

    /*  5
     * Hvordan virker fixed positions for enemy units
     */



}