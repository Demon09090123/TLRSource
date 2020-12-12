using System;
using System.Windows.Forms;
using TerrainGen.Resources;

namespace TerrainGen
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Form1();

            ResourceManager.Initializa();

            Application.Run(form);
        }
    }
}
