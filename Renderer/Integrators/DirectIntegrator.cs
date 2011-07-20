using Renderer.Math;

namespace Renderer.Integrators
{
    public class DirectIntegrator : IIntegrator
    {
        public Color Integrate(Ray ray, Scene scene)
        {
            Color L = new Color();
            Intersection intersection = null;
            if (scene.Intersect(ray, out intersection))
            {
                foreach (Light thisLight in scene.Lights)
                {
                    Math.Point lightPos = null;
                    Normal lightDir = null;
                    Color lightL = thisLight.L(intersection.Point, out lightPos, out lightDir);
                    if (!scene.Intersect(intersection.Point, lightPos))
                    {
                        float dot = System.Math.Max(0, Vector.Dot(intersection.Normal, lightDir));
                        Color texture = new Color(1, 1, 1);
                        if (intersection.Shape.Texture != null)
                        {
                            texture = intersection.Shape.Texture.Lookup(intersection.TextureCoordU, intersection.TextureCoordV);
                        }
                        L += (texture * intersection.Shape.BRDF.f(-ray.Direction, lightDir) * lightL * dot);
                    }
                }
            }
            return L;
        }
    }
}
