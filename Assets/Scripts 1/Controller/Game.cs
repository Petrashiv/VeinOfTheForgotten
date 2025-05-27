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
        [SerializeField] private int mapWidth = 100;
        [SerializeField] private int mapHeight = 100;
        [SerializeField] private int maxRooms = 20;
        [SerializeField] private int roomMaxSize = 13;
        [SerializeField] private int roomMinSize = 7;
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

        private void GenerateMap()
        {
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
    }
}