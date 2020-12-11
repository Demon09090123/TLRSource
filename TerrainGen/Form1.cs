using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TerrainGen
{
    public partial class Form1 : Form
    {

        private int _riverCount;

        private Bitmap _mask;

        private TerrainGenerator _generator;
        private OpenSimplexNoise _simplexNoise;
        private Random _random;

        public Form1()
        {
            _random = new Random();
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _generator = new TerrainGenerator();
            _generator.Load(1024);
            seedBox.Text = _generator.Seed.ToString();
        }

        private Color ApplyMoisture(Color clr, double moistureNoise)
        {
            if (moistureNoise < .35)
                return ControlPaint.Light(clr, .3f);
            else if (moistureNoise < .7)
                return ControlPaint.Light(clr, .1f);
            else
                return ControlPaint.Dark(clr, .2f);
        }

        private void onAddPicture(PictureBox picture)
        {
            if (canvas.Controls.Count > 0)
                canvas.Controls.RemoveAt(0);
            canvas.Controls.Add(picture);
        }

        public delegate void addPicture(Bitmap picture);

        private void resetBtn_Click(object sender, EventArgs e) =>canvas.Controls.RemoveAt(0);
        private void seedBtn_Click(object sender, EventArgs e)
        {
            _generator.RandomSeed();
            seedBox.Text = _generator.Seed.ToString();
        }

        private void sizeBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (int.TryParse(sizeBox.Text, out int size))
                {
                    _generator.Size = size;
                    Console.WriteLine("Enter");
                }

        }

        private void seedBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (long.TryParse(sizeBox.Text, out long seed))
                {
                    _generator.Seed = seed;
                    Console.WriteLine("Enter");
                }
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            if (canvas.Controls.Count > 0)
                canvas.Controls.RemoveAt(0);

            _generator.GenerateTerrain((bitmap) =>
            {
                canvas.BeginInvoke(new addPicture(drawMap), bitmap);
            });
        }

        private void drawMap(Bitmap pic)
        {
            var image = ResizeImage(pic, canvas.Width, canvas.Height);
            canvas.Image = image;
        }

        private void riverTBar_Scroll(object sender, EventArgs e)
        {
            _riverCount = riverTBar.Value;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
