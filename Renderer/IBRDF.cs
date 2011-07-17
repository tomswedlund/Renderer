using Renderer.Math;

namespace Renderer
{
    public interface IBRDF
    {
        float f(Normal wi, Normal wo);
    }
}
