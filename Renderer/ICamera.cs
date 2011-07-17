using Renderer.Math;

namespace Renderer
{
    public interface ICamera
    {
        Ray GenerateRay(Point point);

        void Transform(Transformation trans);
    }
}
