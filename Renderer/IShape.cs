using Renderer.Math;

namespace Renderer
{
    public interface IShape
    {
        IBRDF BRDF { get; }

        bool Intersect(Ray ray, out Intersection intersection);

        void Transform(Transformation trans);
    }
}
