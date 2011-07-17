using Renderer.Math;

namespace Renderer.Cameras
{
    public class OrthogonalCamera : ICamera
    {
        private Transformation _transform = new Transformation();

        public Ray GenerateRay(Point point)
        {
            Ray ray = new Ray(point, new Normal(0, 0, 1));
            ray.Transform(this._transform);
            return ray;
        }

        public void Transform(Transformation trans)
        {
            this._transform.Transform(trans);
        }
    }
}
