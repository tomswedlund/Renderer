using Renderer.Math;
using System.Collections.Generic;

namespace Renderer
{
    public class Scene
    {
        public List<Light> Lights { get; private set; }
        public List<IShape> Shapes { get; private set; }

        public Scene()
        {
            this.Lights = new List<Light>();
            this.Shapes = new List<IShape>();
        }

        public bool Intersect(Ray ray, out Intersection intersection)
        {
            intersection = null;
            foreach (IShape thisShape in this.Shapes)
            {
                if (thisShape.Intersect(ray, out intersection))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
