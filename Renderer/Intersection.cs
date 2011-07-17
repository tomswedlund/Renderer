using Renderer.Math;

namespace Renderer
{
    public class Intersection
    {
        public IShape Shape { get; set; }
        public Point Point { get; set; }
        public Normal Normal { get; set; }
    }
}
