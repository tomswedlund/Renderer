using System.Drawing;

namespace Renderer.Textures
{
    public class ImageTexture : ITexture
    {
        private Bitmap _image = null;

        public ImageTexture(Image image)
        {
            this._image = new Bitmap(image);
        }

        public Color Lookup(float u, float v)
        {
            int iU = (int)(u * this._image.Width);
            int iV = (int)(v * this._image.Height);
            System.Drawing.Color imageColor = this._image.GetPixel(iU, iV);
            Color color = new Color()
            {
                R = imageColor.R / (float)byte.MaxValue,
                G = imageColor.G / (float)byte.MaxValue,
                B = imageColor.B / (float)byte.MaxValue
            };
            return color;
        }
    }
}
