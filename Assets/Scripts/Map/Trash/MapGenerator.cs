using UnityEngine;
using System.Collections.Generic;

public class SimpleMapGenerator : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject[] roomPrefabs;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Vector2 roomSize = new Vector2(20, 20);
    public int roomCount = 10;

    private Dictionary<Vector2Int, GameObject> spawnedRooms = new();

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Queue<Vector2Int> roomQueue = new();
        Vector2Int startPos = Vector2Int.zero;

        GameObject startRoom = SpawnRoom(startPos);
        spawnedRooms[startPos] = startRoom;
        roomQueue.Enqueue(startPos);
        int spawned = 1;

        // Спавним игрока в центре стартовой комнаты
        Vector3 playerSpawnPos = startRoom.transform.position;
        Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
        void Shuffle(Vector2Int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int rnd = Random.Range(i, array.Length);
                (array[i], array[rnd]) = (array[rnd], array[i]);
            }
        }
        while (spawned < roomCount && roomQueue.Count > 0)
        {
            Vector2Int current = roomQueue.Dequeue();
            GameObject currentRoom = spawnedRooms[current];
            RoomDoors doors = currentRoom.GetComponent<RoomDoors>();

            Shuffle(directions);

            foreach (var dir in directions)
            {
                if (!doors.HasDoor(dir)) continue;

                Vector2Int nextPos = current + dir;

                if (spawnedRooms.ContainsKey(nextPos)) continue;

                GameObject newRoom = SpawnRoom(nextPos);
                RoomDoors nextDoors = newRoom.GetComponent<RoomDoors>();

                // Обратное направление тоже должно иметь дверь
                if (!nextDoors.HasDoor(-dir))
                {
                    // Не соединяются — уничтожить комнату
                    Destroy(newRoom);
                    continue;
                }

                spawnedRooms[nextPos] = newRoom;
                roomQueue.Enqueue(nextPos);
                spawned++;

                if (spawned >= roomCount) break;
            }
        }
    }


    GameObject SpawnRoom(Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(
            gridPos.x * roomSize.x,
            gridPos.y * roomSize.y,
            0
        );

        GameObject prefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        GameObject room = Instantiate(prefab, worldPos, Quaternion.identity, transform);
        
        // Спавним врагов, если это не стартовая комната
        if (gridPos != Vector2Int.zero && enemyPrefab != null)
        {

            int enemyCount = Random.Range(1, 4);
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 offset = new Vector2(
                    Random.Range(-roomSize.x / 3, roomSize.x / 3),
                    Random.Range(-roomSize.y / 3, roomSize.y / 3)
                );

                Vector3 spawnPos = worldPos + (Vector3)offset;
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
            
        }


        return room;
    }


    
}
