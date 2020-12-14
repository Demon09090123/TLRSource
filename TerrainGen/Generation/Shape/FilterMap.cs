﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TerrainGen.Generation.Shape
{
    public abstract class FilterMap
    {
        public Bitmap FilterBitmap { get; private set; }
        public int Size { get; private set; }

        public FilterMap(int size)
        {
            Size = size;
            FilterBitmap = new Bitmap(Size, Size);

            generateNoiseMap();
        }

        public Position CenterPosition { get; private set; }
        public void SetCenterPosition(Position pos)
        {
            CenterPosition = pos;
        }
        protected abstract void generateNoiseMap();
    }
}
