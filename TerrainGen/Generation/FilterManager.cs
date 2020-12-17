using System;
using System.Drawing;
using TerrainGen.Generation.Shape;

namespace TerrainGen.Generation
{
    public class FilterManager
    {
        public Bitmap TotalFilterBitmap { get; private set; }
        public int Size { get; private set; }
        public int Quad { get; private set; }
        public FilterManager(int size, int quad)
        {
            Size = size;
            Quad = quad;
            _quadSize = Size / (int)Math.Sqrt(quad);
            TotalFilterBitmap = new Bitmap(Size, Size);
            blankFill();
        }

        private int _quadSize = 0;
        public void Resize(int size, int quadCount)
        {
            TotalFilterBitmap.Dispose();

            Size = size;
            Quad = quadCount;
            _quadSize = Size / Quad;

            TotalFilterBitmap = new Bitmap(Size, Size);
            blankFill();
        }

        private void blankFill()
        {
            using (Graphics gfx = Graphics.FromImage(TotalFilterBitmap))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
                    gfx.FillRectangle(brush, 0, 0, Size, Size);
        }
        public void AddFilter(int qX, int qY, int filterType)
        {
            var rx = qX * _quadSize;
            var ry = qY * _quadSize;
            var qRadius = _quadSize / 2;

            for (var x = 0; x < _quadSize; x++)
            {
                float dX = qRadius - Math.Abs(qRadius - x);

                for (var y = 0; y < _quadSize; y++)
                {
                    float dY = qRadius - Math.Abs(qRadius - y);
                    float delta = Math.Min(dX, dY) / (qRadius - 5.0f);
                    float alpha = delta * 255.0f;

                    if (alpha > 255.0f)
                        alpha = 255.0f;

                    TotalFilterBitmap.SetPixel(rx + x, ry + y, Color.FromArgb((int)alpha, Color.Black));
                }
            }
        }
    }
}
