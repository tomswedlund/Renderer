using Renderer.Integrators;
using Renderer.Math;
using System.Drawing;

namespace Renderer
{
    public static class Renderer
    {
        public static Image Render(Scene scene, ICamera camera, Film film)
        {
            float halfWidth = film.Width / 2;
            float halfHeight = film.Height / 2;
            IIntegrator integrator = new DirectIntegrator();
            for (int x = 0; x < film.Width; ++x)
            {
                for (int y = 0; y < film.Height; ++y)
                {
                    // Transform image->space to camera space
                    Math.Point rayOrigin = new Math.Point()
                    {
                        X = (x - halfWidth) / film.Width,
                        Y = -(y - halfHeight) / film.Width,
                        Z = 0
                    };
                    Ray ray = camera.GenerateRay(rayOrigin);
                    film[x, y] = integrator.Integrate(ray, scene);    
                }
            }
            return film.ToImage();
        }
    }
}
