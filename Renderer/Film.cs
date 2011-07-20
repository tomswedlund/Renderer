using System.Drawing;
using System.Drawing.Imaging;

namespace Renderer
{
    public class Film
    {
        private Color[,] _pixels = null;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color this[int x, int y]
        {
            get
            {
                return this._pixels[x, y];
            }

            set
            {
                this._pixels[x, y] = value;
            }
        }

        public Film(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this._pixels = new Color[width, width];
        }

        public Image ToImage()
        {
            Bitmap image = new Bitmap(this.Width, this.Height);
            for (int x = 0; x < this.Width; ++x)
            {
                for (int y = 0; y < this.Height; ++y)
                {
                    Color color = new Color(this._pixels[x, y]);
                    this.Clamp(color);
                    System.Drawing.Color imageColor =
                        System.Drawing.Color.FromArgb((int)(color.R*255), (int)(color.G*255), (int)(color.B*255));
                    image.SetPixel(x, y, imageColor);
                }
            }
            return image;
        }

        public void Clamp(Color color)
        {
            color.R = System.Math.Max(0, System.Math.Min(1, color.R));
            color.G = System.Math.Max(0, System.Math.Min(1, color.G));
            color.B = System.Math.Max(0, System.Math.Min(1, color.B));
        }
    }
}
