using System;
using System.IO;
using System.Drawing;


namespace Domino
{
    [Serializable]
    public class Tile : IDisposable
    {
        public int Side1 { get; set; }
        public int Side2 { get; set; }
        public bool IsVertical { get; set; } = false;
        public bool IsDouble { get; set; } = false;

        public Tile(int side1, int side2)
        {
            Side1 = side1;
            Side2 = side2;       
            if (side1 == side2)
            {
                IsDouble = true;
            }

        }

        public void Dispose()
        {
            // Освобождаем ресурсы изображения, если они были загружены
            string imagePath = GetImagePath();
            if (File.Exists(imagePath))
            {
                var image = Image.FromFile(imagePath);
                image.Dispose(); // Освобождаем ресурсы изображения
            }
        }
        public string GetImagePath()
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            string fileName;

            if (!IsVertical)
            {
                fileName = $"{Side1}-{Side2}.png";
            }
            else
            {
                fileName = $"{Side1}-{Side2}-vert.png";                
            }

            string fullPath = Path.Combine(basePath, fileName);
            return fullPath;
        }
        public void Flip(Tile tile)
        {
            int side = tile.Side1;
            tile.Side1 = Side2;
            tile.Side2 = side;
        }

    }
}
