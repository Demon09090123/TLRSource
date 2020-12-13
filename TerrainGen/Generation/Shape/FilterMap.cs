using System;
using System.Collections.Generic;
using System.Drawing;

namespace TerrainGen.Generation.Shape
{
    public abstract class FilterMap
    {
        public Bitmap NoiseBitmap { get; private set; }
        public int Size { get; private set; }

        public FilterMap(int size)
        {
            Size = size;
            NoiseBitmap = new Bitmap(Size, Size);

            generateNoiseMap();
        }

        protected abstract void generateNoiseMap();
    }
}
