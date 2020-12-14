using System;
using System.Drawing;
using System.Windows.Forms;
using TerrainGen.Generation.Shape;

namespace TerrainGen
{
    public partial class Form1 : Form
    {
        private Bitmap _mask;
        private MapGeneration _mapGenerator;
        public Form1()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _mapGenerator = new MapGeneration(Properties.Resources.shapeMask);
            _mapGenerator.AddonDraw(drawMap);
            seedBox.Text = _mapGenerator.GetSeed().ToString();
        } 

        public delegate void addPicture(Bitmap picture);

        private void resetBtn_Click(object sender, EventArgs e) =>canvas.Controls.RemoveAt(0);
        private void seedBtn_Click(object sender, EventArgs e)
        {
            _mapGenerator.SetSeed();
            seedBox.Text = _mapGenerator.GetSeed().ToString();
        }

        private void sizeBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (int.TryParse(sizeBox.Text, out int size))
                {
                    _mapGenerator.Resize(size);
                    Console.WriteLine("Enter");
                }

        }

        private void seedBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (long.TryParse(sizeBox.Text, out long seed))
                {
                    _mapGenerator.SetSeed(seed);
                    Console.WriteLine("Enter");
                }
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            _mapGenerator.Generate();
        }

        private delegate void onDraw(Bitmap map);

        private void drawMap(Bitmap pic)
        {
            canvas.BeginInvoke(new onDraw(draw), pic);
        }

        private void draw(Bitmap map)
        {
            canvas.Image = Utils.ResizeImage(map, canvas.Width, canvas.Height); 
        }
        private void addFilterBtn_Click(object sender, EventArgs e)
        {
            _mapGenerator.AddFilter(new CircularFilterMap(600));
        }
    }
}
