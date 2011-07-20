using Renderer.Math;
using System.Drawing;

namespace Renderer
{
    public interface IIntegrator
    {
        Color Integrate(Ray ray, Scene scene);
    }
}
