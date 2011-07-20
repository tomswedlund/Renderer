using System.Drawing;
using System.Drawing.Imaging;

namespace Renderer
{
    public class Film
    {
        private float[,] _pixels = null;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public float this[int x, int y]
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
            this._pixels = new float[width, width];
        }

        public Image ToImage()
        {
            Bitmap image = new Bitmap(this.Width, this.Height);
            for (int x = 0; x < this.Width; ++x)
            {
                for (int y = 0; y < this.Height; ++y)
                {
                    int i = System.Math.Min(255, (int)(this._pixels[x, y] * 255));
                    i = System.Math.Max(i, 0);
                    Color color = Color.FromArgb(i, i, i);
                    image.SetPixel(x, y, color);
                }
            }
            return image;
        }
    }
}
