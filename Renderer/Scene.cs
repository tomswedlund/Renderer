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
            float minT = float.MaxValue;
            bool foundIntersection = false;
            foreach (IShape thisShape in this.Shapes)
            {
                Intersection tempInt = null;
                float tempT = 0;
                if (thisShape.Intersect(ray, out tempInt, out tempT))
                {
                    if (tempT < minT)
                    {
                        intersection = tempInt;
                        minT = tempT;
                    }
                    foundIntersection = true;
                }
            }
            return foundIntersection;
        }

        public bool Intersect(Point frontEnd, Point backEnd)
        {
            Vector segment = frontEnd - backEnd;
            float actualT = segment.Magnitude();
            Normal normSeg = segment.Normalize();
            Point perturbBack = Math.Utils.Perturb(backEnd, normSeg);
            Ray ray = new Ray(perturbBack, normSeg);
            Intersection tempInt = null;
            if (this.Intersect(ray, out tempInt))
            {
                float newT = (tempInt.Point - backEnd).Magnitude();
                return Math.Utils.LessThan(newT, actualT);
            }
            return false;
        }
    }
}
