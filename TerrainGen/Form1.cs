using System;
using System.Drawing;
using System.Windows.Forms;

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

        private void twoBtn_Click(object sender, EventArgs e)
        {
            var grid = new Grid();
            grid.Size = new Size(40, 40);
            grid.Location = new Point(10, 10);
            regionMap.Controls.Add(grid);
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            foreach(Control c in regionMap.Controls)
            {
                var grid = c as Grid;

                _mapGenerator.AddFilter(grid.Location.X, grid.Location.Y, grid.Height);
            }
        }
    }
    public class Grid : PictureBox
    {
        public Grid()
        {
            MouseDown += new MouseEventHandler(onMouseDown);
            MouseMove += new MouseEventHandler(onMouseMove);
            BackColor = Color.Black;
        }

        private Point _onDownPos;
        private void onMouseDown(object sender, MouseEventArgs e) => _onDownPos = e.Location;
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pos = new Point(e.X + Left - _onDownPos.X, e.Y + Top - _onDownPos.Y);

                if (pos.X + Width > 200)
                    pos.X = 200 - Width;
                if (pos.Y + Height > 200)
                    pos.Y = 200 - Height;
                if (pos.X < 0)
                    pos.X = 0;
                if (pos.Y < 0)
                    pos.Y = 0;

                Location = pos;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                if (pos.X >= this.ClientSize.Width - grab && pos.Y >= this.ClientSize.Height - grab)
                    m.Result = new IntPtr(17);  // HT_BOTTOMRIGHT

                var max = Math.Max(Width, Height);
                Width = max;
                Height = max;
            }
        }
        private const int grab = 16;
    }
}
