using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TerrainGen
{
    //CREDITS: https://gist.github.com/digitalshadow/134a3a02b67cecd72181
    public class OpenSimplexNoise
    {
        private const float STRETCH_2D = -0.211324865405187f;    //(1/Math.sqrt(2+1)-1)/2;
        private const float SQUISH_2D = 0.366025403784439f;      //(Math.sqrt(2+1)-1)/2;
        private const float NORM_2D = 1.0f / 47.0f;

        private byte[] perm;
        private byte[] perm2D;

        private static float[] gradients2D = new float[]
        {
             5,  2,    2,  5,
            -5,  2,   -2,  5,
             5, -2,    2, -5,
            -5, -2,   -2, -5,
        };

        private static Contribution2[] lookup2D;

        static OpenSimplexNoise()
        {
            var base2D = new int[][]
            {
                new int[] { 1, 1, 0, 1, 0, 1, 0, 0, 0 },
                new int[] { 1, 1, 0, 1, 0, 1, 2, 1, 1 }
            };
            var p2D = new int[] { 0, 0, 1, -1, 0, 0, -1, 1, 0, 2, 1, 1, 1, 2, 2, 0, 1, 2, 0, 2, 1, 0, 0, 0 };
            var lookupPairs2D = new int[] { 0, 1, 1, 0, 4, 1, 17, 0, 20, 2, 21, 2, 22, 5, 23, 5, 26, 4, 39, 3, 42, 4, 43, 3 };

            var contributions2D = new Contribution2[p2D.Length / 4];
            for (int i = 0; i < p2D.Length; i += 4)
            {
                var baseSet = base2D[p2D[i]];
                Contribution2 previous = null, current = null;
                for (int k = 0; k < baseSet.Length; k += 3)
                {
                    current = new Contribution2(baseSet[k], baseSet[k + 1], baseSet[k + 2]);
                    if (previous == null)
                    {
                        contributions2D[i / 4] = current;
                    }
                    else
                    {
                        previous.Next = current;
                    }
                    previous = current;
                }
                current.Next = new Contribution2(p2D[i + 1], p2D[i + 2], p2D[i + 3]);
            }

            lookup2D = new Contribution2[64];
            for (var i = 0; i < lookupPairs2D.Length; i += 2)
            {
                lookup2D[lookupPairs2D[i]] = contributions2D[lookupPairs2D[i + 1]];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FastFloor(float x)
        {
            var xi = (int)x;
            return x < xi ? xi - 1 : xi;
        }

        public OpenSimplexNoise()
            : this(DateTime.Now.Ticks)
        {
        }

        public OpenSimplexNoise(long seed)
        {
            perm = new byte[256];
            perm2D = new byte[256];
            var source = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                source[i] = (byte)i;
            }
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            for (int i = 255; i >= 0; i--)
            {
                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int r = (int)((seed + 31) % (i + 1));
                if (r < 0)
                {
                    r += (i + 1);
                }
                perm[i] = source[r];
                perm2D[i] = (byte)(perm[i] & 0x0E);
                source[r] = source[i];
            }
        }

        public enum GenerationType
        {
            Normal = 0,
            Rigid = 1
        }

        public float[,] Noise2D(int width, int height, float scale)
        {
            float[,] noise2D = new float[width, height];
            float min = float.MaxValue;
            float max = float.MinValue;
            int xOff = MapGeneration._random.Next(0, width * 10);
            int yOff = MapGeneration._random.Next(0, height * 10);

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    float n = Evaluate(x + xOff, y + yOff, scale);

                    if (n > max)
                        max = n;
                    else if (n < min)
                        min = n;

                    noise2D[x, y] = n;
                }

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var n = noise2D[x, y];
                    var inLerp = Utils.clamp((n - min) / (max - min));
                    noise2D[x, y] = inLerp;
                }

            return noise2D;
        }

        public float[,] Noise2D(int width, int height, float scale, int octaves, float persistence = 0.5f,
            float lacunarity = 2.0f, GenerationType type = GenerationType.Normal)
        {
            float[,] noise2D = new float[width, height];

            float min = float.MaxValue;
            float max = float.MinValue;
            float radWidth = width / 2;
            float radHeight = height / 2;
            int xOff = MapGeneration._random.Next(0, width * 10);
            int yOff = MapGeneration._random.Next(0, height * 10);

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    float n = 0.0f;
                    switch(type)
                    {
                        case GenerationType.Normal:
                            n = Noise(x + xOff - radWidth, y - radHeight, scale, octaves, persistence, lacunarity);
                            break;
                        case GenerationType.Rigid:
                            n = RigidNoise(x + yOff - radWidth, y - radHeight, scale, octaves, persistence, lacunarity);
                            break;
                    }

                    if (n > max)
                        max = n;
                    else if (n < min)
                        min = n;

                    noise2D[x, y] = n;
                }

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var n = noise2D[x, y];
                    var inLerp = Utils.clamp((n - min) / (max - min));
                    noise2D[x, y] = inLerp;
                }

            return noise2D;
        }

        public float RigidNoise(float x, float y, float scale, int octave, float persistence = 0.5f, float lacunarity = 2.0f)
        {
            float sum = 1.0f - Evaluate(x, y, scale);
            float amplitude = 1.0f;
            float frequency = 1.0f;

            for (var o = 0; o < octave; o++)
            {
                amplitude *= persistence;
                sum -= (1 - Evaluate(x * frequency, y * frequency, scale)) * amplitude;
                frequency *= lacunarity;
            }

            return sum;
        }
        public float Noise(float x, float y, float scale, int octaves, float persistence = 0.5f, float lacunarity = 2.0f)
        {
            float total = 0;
            float frequency = 1.0f;
            float amplitude = 1.0f;

            for (int i = 0; i < octaves; i++)
            {
                total += Evaluate(x * frequency, y * frequency, scale) * amplitude;

                amplitude *= persistence;
                frequency *= lacunarity;
            }

            return total;
        }

        public float Evaluate(float x, float y, float scale) => Evaluate(x * scale, y * scale);

        public float Evaluate(float x, float y)
        {
            var stretchOffset = (x + y) * STRETCH_2D;
            var xs = x + stretchOffset;
            var ys = y + stretchOffset;

            var xsb = FastFloor(xs);
            var ysb = FastFloor(ys);

            var squishOffset = (xsb + ysb) * SQUISH_2D;
            var dx0 = x - (xsb + squishOffset);
            var dy0 = y - (ysb + squishOffset);

            var xins = xs - xsb;
            var yins = ys - ysb;

            var inSum = xins + yins;

            var hash =
               (int)(xins - yins + 1) |
               (int)(inSum) << 1 |
               (int)(inSum + yins) << 2 |
               (int)(inSum + xins) << 4;

            var c = lookup2D[hash];

            var value = 0.0f;
            while (c != null)
            {
                var dx = dx0 + c.dx;
                var dy = dy0 + c.dy;
                var attn = 2 - dx * dx - dy * dy;
                if (attn > 0)
                {
                    var px = xsb + c.xsb;
                    var py = ysb + c.ysb;

                    var i = perm2D[(perm[px & 0xFF] + py) & 0xFF];
                    var valuePart = gradients2D[i] * dx + gradients2D[i + 1] * dy;

                    attn *= attn;
                    value += attn * attn * valuePart;
                }
                c = c.Next;
            }

            return value * NORM_2D;
        }

        private class Contribution2
        {
            public float dx, dy;
            public int xsb, ysb;
            public Contribution2 Next;

            public Contribution2(float multiplier, int xsb, int ysb)
            {
                dx = -xsb - multiplier * SQUISH_2D;
                dy = -ysb - multiplier * SQUISH_2D;
                this.xsb = xsb;
                this.ysb = ysb;
            }
        }
    }
}
