namespace Renderer
{
    public interface ITexture
    {
        Color Lookup(float u, float v);
    }
}
