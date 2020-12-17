using System;
using System.Drawing;
using TerrainGen.Generation.Shape;

namespace TerrainGen.Generation
{
    public class FilterManager
    {
        public Bitmap TotalFilterBitmap { get; private set; }
        public int Size { get; private set; }
        public FilterManager(int size)
        {
            Size = size;
            TotalFilterBitmap = new Bitmap(Size, Size);
            blankFill();
        }

        public void Resize(int size)
        {
            TotalFilterBitmap.Dispose();

            Size = size;

            TotalFilterBitmap = new Bitmap(Size, Size);
            blankFill();
        }

        private void blankFill()
        {
            using (Graphics gfx = Graphics.FromImage(TotalFilterBitmap))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
                    gfx.FillRectangle(brush, 0, 0, Size, Size);
        }

        public void AddFilter(int x, int y, int width, int height)
        {
            var scale = Size / 200.0f;
            var mapX = x * scale;
            var mapY = y * scale;
            var mapWidth = width * scale;
            var mapHeight = height * scale;
            var radiusX = mapWidth / 2;
            var radiusY = mapHeight / 2;

            for (int rX = 0; rX < mapWidth; rX++)
            {
                var dX = radiusX - Math.Abs(radiusX - rX);

                for (int rY = 0; rY < mapHeight; rY++)
                {
                    var dY = radiusY - Math.Abs(radiusY - rY);
                    var dMin = Math.Min(dX, dY);
                    var dSize = Math.Min(radiusX, radiusY);

                    var delta = dMin / dSize;
                    var alpha = delta * 255.0f;

                    if (alpha > 255.0f)
                        alpha = 255.0f;

                    TotalFilterBitmap.SetPixel((int)mapX + rX, (int)mapY + rY, Color.FromArgb((int)alpha, Color.Black));
                }
            }
        }
        
    }
}
