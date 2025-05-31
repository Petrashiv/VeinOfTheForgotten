using System;
using System.Collections.Generic;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Utilities;
using RogueSharp.Random;
using Unity.VisualScripting;
using UnityEngine;
using RogueSharp;
using UnityEngine.LightTransport;
//using UnityEngine;

namespace RogueSharpTutorial.Controller
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject lukePrefab;
        [SerializeField] private GameObject chestPrefab;

        [SerializeField] private int mapWidth = 100;
        [SerializeField] private int mapHeight = 100;
        [SerializeField] private int maxRooms = 20;
        [SerializeField] private int roomMaxSize = 13;
        [SerializeField] private int roomMinSize = 7;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemy;

        private Point lukePoint;
        public static IRandom Random { get; private set; }

        private DungeonMap dungeonMap;

        public Game(UI_Main console)
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            //GenerateMap();
        }
        void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);
            // Создаем генератор карты через конструктор (как у вас было изначально)
            MapGenerator mapGenerator = new MapGenerator(
                this,
                mapWidth,
                mapHeight,
                maxRooms,
                roomMaxSize,
                roomMinSize,
                1 // mapLevel
            );
            

            dungeonMap = mapGenerator.CreateMap();
            DrawMap();

            int X = dungeonMap.Rooms[0].Center.X;
            int Y = dungeonMap.Rooms[0].Center.Y;
            Vector2 startPos = new Vector2(X, Y);

            player.transform.position = startPos;

            PlaceEnemies();
            PlaceLuke();
            PlaceChests();
        }
        private void DrawMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    ICell cell = dungeonMap.GetCell(x, y);
                    Vector2 position = new Vector2(x, y);

                    GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity);
                    TileUnity tile = tileObj.GetComponent<TileUnity>();

                    if (cell.IsWalkable)
                    {
                        tile.BackgroundColor = Color.gray;
                        tile.Collider.enabled = false;
                    }
                    else
                    {
                        tile.BackgroundColor = Color.black;
                        tile.Collider.enabled = true;
                    }
                }
            }
        }

        private void PlaceEnemies()
        {
            for (int i = 1; i < dungeonMap.Rooms.Count; i++) // комнаты
            {
                int enemyCount = Random.Next(0, 3);

                for (int j = 0; j < enemyCount; j++) // враги
                {
                    int X = dungeonMap.GetRandomWalkableLocationInRoom(dungeonMap.Rooms[i]).X;
                    int Y = dungeonMap.GetRandomWalkableLocationInRoom(dungeonMap.Rooms[i]).Y;

                    GameObject newEnemy = Instantiate(enemy, new Vector2 (X, Y), Quaternion.identity);
                    newEnemy.SetActive(true);
                }
            }
        }

        private void PlaceLuke()
        {
            lukePoint = dungeonMap.GetRandomWalkableLocationInRoom(dungeonMap.Rooms[Random.Next(1, dungeonMap.Rooms.Count - 1)]);
            Vector2 lukePos = new Vector2(lukePoint.X, lukePoint.Y);

            Instantiate(lukePrefab, lukePos, Quaternion.identity);
        }

        private void PlaceChests()
        {
            int chestsCount = Random.Next(0, 3);

            for (int i = 0; i < chestsCount; i++)
            {
                Point chestPoint = dungeonMap.GetRandomWalkableLocationInRoom(dungeonMap.Rooms[Random.Next(1, dungeonMap.Rooms.Count - 1)]);

                if (chestPoint == lukePoint)
                {
                    continue;
                }

                Vector2 chestPos = new Vector2(chestPoint.X, chestPoint.Y);
                Instantiate(chestPrefab, chestPos, Quaternion.identity);
            }
        }
    }
}