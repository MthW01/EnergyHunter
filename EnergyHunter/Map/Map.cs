using System.Collections.Generic;

namespace EnergyHunter
{
    class Map
    {
        private List<ColissionTiles> colissionTiles = new List<ColissionTiles>();
        public List<ColissionTiles> ColissionTiles { get { return colissionTiles; } }

        private int width, height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Map() { }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0)
                        colissionTiles.Add(new ColissionTiles(number, new Rectangle(x * size, y * size, size, size)));

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ColissionTiles tile in colissionTiles)
                tile.Draw(spriteBatch);
        }
    }
}
