using System;
using System.Drawing;
using System.Windows.Forms;

namespace TerrainGen
{
    public partial class Form1 : Form
    {
        public float MapScale;
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        private OpenSimplexNoise SimplexNoise;

        public Form1()
        {
            InitializeComponent();

            MapWidth = 0;
            MapHeight = 0;
            MapScale = 0f;

            SimplexNoise = new OpenSimplexNoise();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }


        /*        private double[,] createIslandMask()
                {
                    var mask = new double[Width, Height];
                    int radius = Width / 2;

                    for (var x = 0; x < Width; x++)
                    {
                        var xDist = 
                        for (var y = 0; y < 0; y++)
                        {



                        }
                    }



                }*/


        private void generateHeightMap()
        {
            var bitmap = new Bitmap(MapWidth, MapHeight);
            var zeroCounter = 0;

            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    double noise = SimplexNoise.Evaluate(x * 0.06, y * 0.06);

                    if (noise < 0)
                    {
                        noise = 0;
                        zeroCounter++;
                    }

                    bitmap.SetPixel((int)x, (int)y, Color.FromArgb((int)(255 * noise ), 0, 0, 0));
                }
            }
            var pictureBox = new PictureBox();
            pictureBox.Image = bitmap;
            pictureBox.Width = bitmap.Width;
            pictureBox.Height = bitmap.Height;
            canvas.Controls.Add(pictureBox);
            Console.WriteLine(zeroCounter);
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            if (MapWidth > 0 && MapHeight > 0)
                generateHeightMap();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                MapWidth = int.Parse(textBox1.Text);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                MapHeight = int.Parse(textBox2.Text);
        }

        private void scaleBar_Scroll(object sender, EventArgs e)
        {
            var mapScale = scaleBar.Value / 100f;
            canvas.Controls[0].Scale(new SizeF(mapScale, mapScale));
        }
    }
}
