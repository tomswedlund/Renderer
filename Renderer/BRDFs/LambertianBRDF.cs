using Renderer.Math;

namespace Renderer.BRDFs
{
    public class LambertianBRDF : IBRDF
    {
        public float f(Normal wi, Normal wo)
        {
            return (float)(1 / System.Math.PI);
        }
    }
}
