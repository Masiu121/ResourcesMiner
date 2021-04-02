using Microsoft.Xna.Framework.Graphics;

namespace ResourcesMiner
{
    public class GameTile
    {
        public int Type;
        public int Hardness;
        public bool IsMined;
        public Texture2D Texture;

        public GameTile(int type)
        {
            Type = type;
        }

        public void SetHardness()
        {
            if (Type == 0 || Type == 13)
                Hardness = 0;
            if (Type == 1 || Type == 2)
                Hardness = 1;
            if (Type == 3)
                Hardness = 2;
            if (Type == 4)
                Hardness = 4;
            if (Type == 5 || Type == 6)
                Hardness = 2;
            if (Type == 7 || Type == 8)
                Hardness = 3;
            if (Type == 9 || Type == 10)
                Hardness = 4;
            if (Type == 11 || Type == 12)
                Hardness = 4;
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