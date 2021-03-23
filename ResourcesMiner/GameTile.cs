using Microsoft.Xna.Framework.Graphics;

namespace ResourcesMiner
{
    public class GameTile
    {
        public Location Location;
        public int Type;
        public bool HasMiner;
        public bool IsMined;
        public Texture2D Texture;

        public GameTile(int type, Location location)
        {
            Type = type;
            Location = location;
        }

        public GameTile(int type, bool hasMiner)
        {
            Type = type;
            HasMiner = hasMiner;
        }
    }

    public class Location
    {
        public int x, y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}