using Renderer.Math;

namespace Renderer
{
    public interface IShape
    {
        IBRDF BRDF { get; }

        bool Intersect(Ray ray, out Intersection intersection, out float t);

        void Transform(Transformation trans);
    }
}
