using System.Collections;
using System.Collections.Generic;

namespace ResourcesMiner
{
    public class Map
    {
        private GameTile[,] map;
        private List<MapComponent> _mapComponents;
        
        public Map(int mapWidth)
        {
            map = new GameTile[mapWidth, mapWidth];
            _mapComponents = new List<MapComponent>();
        }

        private void AddComponent(GameTile tile, int genMin, int genMax, float chance, int priority)
        {
            MapComponent component = new MapComponent(tile, genMin, genMax, chance, priority);
            _mapComponents.Add(component);
        }
    }

    public class MapComponent
    {
        private GameTile _tile;
        private int _generationMin;
        private int _generationMax;
        private float _chance;
        private int _priority;

        public MapComponent(GameTile tile, int genMin, int genMax, float chance, int priority)
        {
            _tile = tile;
            _generationMin = genMin;
            _generationMax = genMax;
            _chance = chance;
            _priority = priority;
        }
    }
}