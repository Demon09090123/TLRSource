using System;
using System.Drawing;
using System.Windows.Forms;
using TerrainGen.Generation.Shape;

namespace TerrainGen
{
    public partial class Form1 : Form
    {
        private MapGeneration _mapGenerator;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _mapGenerator = new MapGeneration();
            _mapGenerator.AddonDraw(drawMap);
            seedBox.Text = _mapGenerator.GetSeed().ToString();

            refreshAndUpdateGraph(2);
        }

        public delegate void addPicture(Bitmap picture);

        private void resetBtn_Click(object sender, EventArgs e) => canvas.Controls.RemoveAt(0);
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

        private int _curQuadSize = 4;
        private void refreshAndUpdateGraph(int quadCount)
        {
            regionMap.Controls.Clear();

            _curQuadSize = (int)Math.Sqrt(quadCount);
            var w = regionMap.Width;
            var h = regionMap.Height;
            var gridSize = w / _curQuadSize;

            _mapGenerator.ResetFilter(_curQuadSize);

            for (var x = 0; x < _curQuadSize; x++)
                for (var y = 0; y < _curQuadSize; y++)
                {
                    var gX = x * gridSize;
                    var gY = y * gridSize;

                    var grid = new Grid(x, y, onSelectRegion);
                    grid.Size = new Size(gridSize, gridSize);
                    grid.Location = new Point(gX, gY);
                    regionMap.Controls.Add(grid);
                }
        }

        private void onSelectRegion(Grid g)
        {
            _mapGenerator.AddFilter(g.GridX, g.GridY, 0);
            Console.WriteLine($"FilterAdded! {g.GridX} {g.GridY}");
        }

        private void twoBtn_Click(object sender, EventArgs e) => refreshAndUpdateGraph(4);
        private void fourbtn_Click(object sender, EventArgs e) => refreshAndUpdateGraph(16);
        private void eightBtn_Click(object sender, EventArgs e) => refreshAndUpdateGraph(64);
        private void sixteenBtn_Click(object sender, EventArgs e) => refreshAndUpdateGraph(256);
    }
    public class Grid : PictureBox
    {
        private static readonly Color onSelected = Color.Black;
        private static readonly Color notSelected = Color.Gray;

        public int GridX { get; private set; }
        public int GridY { get; private set; }

        private Action<Grid> _onSelectRegion;

        public Grid(int x, int y, Action<Grid> onSelect)
        {
            Click += new EventHandler(onGridClick);
            BackColor = notSelected;
            BorderStyle = BorderStyle.FixedSingle;
            GridX = x;
            GridY = y;
            _onSelectRegion = onSelect;
        }

        private void onGridClick(object sender, EventArgs e)
        {
            if (BackColor == onSelected)
            {
                BackColor = notSelected;
                return;
            }
            BackColor = onSelected;
            _onSelectRegion.Invoke(this);
        }
    }
}
