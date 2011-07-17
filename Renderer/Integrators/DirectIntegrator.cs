using Renderer.Math;

namespace Renderer.Integrators
{
    public class DirectIntegrator : IIntegrator
    {
        public float Integrate(Ray ray, Scene scene)
        {
            float L = 0;
            Intersection intersection = null;
            if (scene.Intersect(ray, out intersection))
            {
                foreach (Light thisLight in scene.Lights)
                {
                    Normal lightDir = null;
                    float lightL = thisLight.L(intersection.Point, out lightDir);
                    L += (intersection.Shape.BRDF.f(-ray.Direction, lightDir) * lightL * System.Math.Max(0, Vector.Dot(intersection.Normal, lightDir)));
                }
            }
            return L;
        }
    }
}
