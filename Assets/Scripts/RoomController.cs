using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    public GameObject player;
    private GameObject currentRoom;
    private int combatRoomCounter = 0;
    
    public GameObject[] combatRoomPrefabs;
    private List<GameObject> combatRooms = new List<GameObject>();
    
    public GameObject lobbyRoomPrefab;
    private GameObject lobby;
    
    public GameObject shopRoomPrefab;
    private GameObject shop;
    
    public GameObject bossRoomPrefab;
    private GameObject bossRoom;
    
    public GameObject bossShopRoomPrefab;
    private GameObject bossShopRoom;
    void Start()
    {
        InitializeRooms();
    }

    
    private void InitializeRooms()
    {
        for (int i = 0; i < combatRoomPrefabs.Length; i++)
        {
            // Making object pool
            GameObject newRoom = Instantiate(combatRoomPrefabs[i], new Vector3(0,0,0), Quaternion.identity);
            newRoom.SetActive(false); // Deactivate the room initially
            combatRooms.Add(newRoom);
        }
        //Instantiate lobby, shop, boss room and boss shop room
        lobby = Instantiate(lobbyRoomPrefab, new Vector3(0,0,0), Quaternion.identity);
        lobby.SetActive(false);
        shop = Instantiate(shopRoomPrefab, new Vector3(0,0,0), Quaternion.identity);
        shop.SetActive(false);
        bossRoom = Instantiate(bossRoomPrefab, new Vector3(0,0,0), Quaternion.identity);
        bossRoom.SetActive(false);
        bossShopRoom = Instantiate(bossShopRoomPrefab, new Vector3(0,0,0), Quaternion.identity);
        bossShopRoom.SetActive(false);
        
        //Start in lobby
        SpawnLobbyRoom();
    }
    private void SpawnCombatRoom()
    {
        foreach (var room in combatRooms)
        {
            room.SetActive(false);
        }
        
        int randomRoom = Random.Range(0, combatRooms.Count);
        GameObject selectedRoom = combatRooms[randomRoom];
        combatRoomCounter++;
        
        selectedRoom.SetActive(true);
        currentRoom = selectedRoom;
        Transform spawnPoint = selectedRoom.transform.Find("SpawnPoint");
        player.transform.position = spawnPoint.position;
    }
    
    private void SpawnShopRoom()
    {
        shop.SetActive(true);
        currentRoom = shop;
        Transform spawnPoint = shop.transform.Find("SpawnPoint");
        player.transform.position = spawnPoint.position;
    }
    
    private void SpawnLobbyRoom()
    {
        lobby.SetActive(true);
        currentRoom = lobby;
        Transform spawnPoint = lobby.transform.Find("SpawnPoint");
        player.transform.position = spawnPoint.position;
    }
    
    private void SpawnBossRoom()
    {
        //Spawn boss room
    }
    
    private void SpawnBossShopRoom()
    {
        //Spawn boss shop room
    }
    
    
    
    public void NextRoom()
    {
        if (currentRoom.CompareTag("LobbyRoom"))
        {
            SpawnCombatRoom();
            lobby.SetActive(false);
        }else if (currentRoom.CompareTag("CombatRoom"))
        {
            SpawnShopRoom();
            foreach (var room in combatRooms)
            {
                room.SetActive(false);
            }
        }else if (currentRoom.CompareTag("ShopRoom"))
        {
            SpawnCombatRoom();
            shop.SetActive(false);
        }else if (combatRoomCounter >= 5)
        {
            SpawnBossRoom();
            foreach (var room in combatRooms)
            {
                room.SetActive(false);
            }
        }else if (currentRoom.CompareTag("BossRoom"))
        {
            SpawnBossShopRoom();
            bossRoom.SetActive(false);
        }else if (currentRoom.CompareTag("BossShopRoom"))
        {
            SpawnCombatRoom();
            bossShopRoom.SetActive(false);
        }
    }
}
