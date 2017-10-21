using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace FractCSharp
{
    public partial class FormFractal : Form
    {
        public const int MaxIterations = 50;
        public const double ScalingFactor = 1.0 / 200.0;

        public static Complex MapPlane(int x, int y)
        {
            return new Complex(x * ScalingFactor - 2.0, y * ScalingFactor - 1.0);
        }

        public static Color[] Colors =
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Indigo,
            Color.Purple,
        };

        public FormFractal()
        {
            InitializeComponent();
        }

        public static Bitmap BuildMandelbrotImage(int width,int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int iteration = 0;

                    Complex current = MapPlane(x, y), temp = current;
                    
                    while(temp.Magnitude<=2.0 && iteration < MaxIterations)
                    {
                        temp = temp * temp + current;

                        iteration++;
                    }
                    {
                        double ratio =1.0 -  (double)iteration / (double)MaxIterations;
                        Color rc = Color.FromArgb((int)(255 * ratio), (int)(255 * ratio), (int)(255 * ratio));
                        bitmap.SetPixel(x, y, rc);
                    }
                }

            }

            return bitmap;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(BuildMandelbrotImage(this.Width, this.Height), 0, 0);
            base.OnPaint(e);
        }
    }
}
