using Renderer.Math;

namespace Renderer
{
    public class Light
    {
        public Color E { get; private set; }
        public Point Position { get; private set; }

        public Light(Color e)
        {
            this.E = e;
            this.Position = new Point();
        }

        public Color L(Point point, out Point lightPos, out Normal lightDir)
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
