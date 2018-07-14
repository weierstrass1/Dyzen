using System.Drawing;
using System.Windows.Forms;
using backend;
using System.IO;
using System;

namespace Dyzen
{
    public partial class Dyzen : Form
    {
        private static Zoom zoom = Zoom.x4;

        public static Zoom Zoom
        {
            set
            {
                if(zoom!=value)
                {
                    zoom = value;
                    ZoomChanged?.Invoke();
                }
            }
        }

        public static int IntZoom { get { return (int)zoom; } }

        public static event Action ZoomChanged;
        public Dyzen()
        {
            InitializeComponent();

            ZoomChanged += Dyzen_ZoomChanged;

            ColorPalette.GeneratePalette("Doom3.pal", 16);

            gfxBox1.GetTiles("Doom3.bin", TileSize.Size16x16, BaseTile.Top);
        }

        private void Dyzen_ZoomChanged()
        {
            gfxBox1.Zoom = IntZoom;
        }
    }
}
