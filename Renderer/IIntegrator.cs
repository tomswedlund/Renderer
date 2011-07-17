using Renderer.Math;

namespace Renderer
{
    public interface IIntegrator
    {
        float Integrate(Ray ray, Scene scene);
    }
}
