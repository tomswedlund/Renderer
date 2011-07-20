using Renderer.Math;

namespace Renderer
{
    public class Light
    {
        public float E { get; private set; }
        public Point Position { get; private set; }

        public Light(float e)
        {
            this.E = e;
            this.Position = new Point();
        }

        public float L(Point point, out Point lightPos, out Normal lightDir)
        {
            Vector dir = this.Position - point;
            float dist = dir.Magnitude();
            lightDir = new Normal(dir);
            lightPos = this.Position;
            return (this.E / (dist * dist));
        }

        public void Transform(Transformation trans)
        {
            this.Position = this.Position.Transform(trans);
        }
    }
}
