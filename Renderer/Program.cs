using System;
using System.Drawing;
using Renderer.BRDFs;
using Renderer.Cameras;
using Renderer.Math;
using Renderer.Shapes;
using Renderer.Textures;

namespace Renderer
{
    class Program
    {
        static void Main(string[] args)
        {
            ICamera camera = new PerspectiveCamera(45);
            Film film = new Film(500, 500);
            
            Scene scene = new Scene();
            Light light = new Light(new Color(10, 10, 10));
            light.Transform(new Transformation().Translate(1.5f, 0, -1.5f));
            scene.Lights.Add(light);
            IShape shape = new SphereShape(0.2f, new LambertianBRDF(), new HomogeneousTexture(new Color(1, 1, 0)));
            shape.Transform(new Transformation().Translate(0.2f, -0.2f, 0.4f));
            scene.Shapes.Add(shape);
            shape = new PlaneShape(new LambertianBRDF(), null);
            shape.Transform(new Transformation().Scale(1.2f, 1.2f, 1.2f).Translate(0, 0, 1).Rotate(45, 1, 0, 0));
            scene.Shapes.Add(shape);
    
            Image image = Renderer.Render(scene, camera, film);
            image.Save(@"image.bmp");
        }
    }
}
