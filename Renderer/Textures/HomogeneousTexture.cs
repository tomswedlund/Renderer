namespace Renderer.Textures
{
    public class HomogeneousTexture : ITexture
    {
        private Color _color;

        public HomogeneousTexture(Color color)
        {
            this._color = color;
        }

        public Color Lookup(float u, float v)
        {
            return this._color;
        }
    }
}
