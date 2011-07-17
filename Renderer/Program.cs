using System;
using System.Drawing;
using Renderer.BRDFs;
using Renderer.Cameras;
using Renderer.Math;
using Renderer.Shapes;

namespace Renderer
{
    class Program
    {
        static void Main(string[] args)
        {
            ICamera camera = new OrthogonalCamera();
            Film film = new Film(100, 100);
            
            Scene scene = new Scene();
            Light light = new Light(200);
            light.Transform(new Transformation().Translate(5, 5, 0));
            scene.Lights.Add(light);
            IShape shape = new SphereShape(0.4f, new LambertianBRDF());
            shape.Transform(new Transformation().Translate(0, 0, 10));
            scene.Shapes.Add(shape);


            Image image = Renderer.Render(scene, camera, film);
            image.Save(@"image.bmp");
        }
    }
}
