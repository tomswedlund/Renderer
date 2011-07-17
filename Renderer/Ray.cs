using Renderer.Math;

namespace Renderer
{
    public class Ray
    {
        public Point Origin { get; set; }
        public Normal Direction { get; set; }

        public Point this[float t]
        {
            get
            {
                return (this.Origin + this.Direction * t);
            }
        }

        public Ray(Point origin, Normal direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public void Transform(Transformation trans)
        {
            this.Origin = this.Origin.Transform(trans);
            this.Direction = this.Direction.Transform(trans);
        }
    }
}
