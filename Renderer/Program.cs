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
            ICamera camera = new PerspectiveCamera(45);
            Film film = new Film(500, 500);
            
            Scene scene = new Scene();
            Light light = new Light(1);
            light.Transform(new Transformation().Translate(0.6f, 0, -0.3f));
            scene.Lights.Add(light);
            IShape shape = new SphereShape(0.2f, new LambertianBRDF());
            shape.Transform(new Transformation().Translate(0.2f, -0.1f, 0.4f));
            scene.Shapes.Add(shape);
            shape = new PlaneShape(new LambertianBRDF());
            shape.Transform(new Transformation().Scale(1.2f, 1.2f, 1.2f).Translate(0, 0, 1).Rotate(45, 1, 0, 0));
            scene.Shapes.Add(shape);
    
            Image image = Renderer.Render(scene, camera, film);
            image.Save(@"image.bmp");
        }
    }
}
