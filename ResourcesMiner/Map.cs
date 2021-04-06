using System;
using System.Collections.Generic;

namespace ResourcesMiner
{
    public class Map
    {
        public GameTile[,] map;
        public List<MapComponent> _mapComponents;
        private int _mapWidth;
        private int _mapHeight;

        public Map(int mapWidth, int mapHeight)
        {
            map = new GameTile[mapWidth, mapHeight];
            _mapComponents = new List<MapComponent>();
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void AddComponent(GameTile tile, int genMin, int genMax, double chance)
        {
            MapComponent component = new MapComponent(tile, genMin, genMax, chance);
            _mapComponents.Add(component);
        }

        public void GenerateMap()
        {
            Random rand = new Random();
            double chance;
            for(int a = 0; a < _mapComponents.Count; a++)
            {
                for (int i = 0; i < _mapWidth; i++)
                {
                    for (int j = 0; j < _mapHeight; j++)
                    {
                        if (i == 0 || i == _mapWidth - 1 || j == _mapHeight - 1)
                        {
                            map[i, j] = new GameTile(14);
                        }
                        else
                        {
                            if (j >= _mapComponents[a].GenerationMin && j <= _mapComponents[a].GenerationMax)
                            {
                                chance = rand.NextDouble();
                                if (chance <= _mapComponents[a].Chance)
                                {
                                    map[i, j] = new GameTile(_mapComponents[a].Tile.Type);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class MapComponent
    {
        public GameTile Tile;
        public int GenerationMin;
        public int GenerationMax;
        public double Chance;

        public MapComponent(GameTile tile, int genMin, int genMax, double chance)
        {
            Tile = tile;
            GenerationMin = genMin;
            GenerationMax = genMax;
            Chance = chance;
        }
    }
}