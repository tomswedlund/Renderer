using Renderer.Math;

namespace Renderer.Cameras
{
    public class PerspectiveCamera : ICamera
    {
        private Point _focalPoint = null;
        private Transformation _transform = new Transformation();

        public PerspectiveCamera(float fov)
        {
            float dist = 0.5f / (float)System.Math.Tan(fov / 2.0f);
            this._focalPoint = new Point(0, 0, -dist);
        }

        public Ray GenerateRay(Point point)
        {
            Ray ray = new Ray(point, new Normal(point - this._focalPoint));
            ray.Transform(this._transform);
            return ray;
        }

        public void Transform(Transformation trans)
        {
            this._transform = this._transform.Transform(trans);
        }
    }
}
