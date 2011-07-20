using Renderer.Math;

namespace Renderer.Shapes
{
    public class SphereShape : IShape
    {
        private float _radius;
        private Point _center = new Point();

        public IBRDF BRDF { get; private set; }
        public ITexture Texture { get; private set; }

        public SphereShape(float radius, IBRDF brdf, ITexture texture)
        {
            this._radius = radius;
            this.BRDF = brdf;
            this.Texture = texture;
        }

        public bool Intersect(Ray ray, out Intersection intersection, out float t)
        {
            float a = Vector.Dot(ray.Direction, ray.Direction);
            Vector omc = ray.Origin - this._center;
            float b = 2 * Vector.Dot(ray.Direction, omc);
            float c = Vector.Dot(omc, omc) - (this._radius * this._radius);

            float sqr = b * b - 4 * c;
            if (sqr < 0)
            {
                intersection = null;
                t = 0;
                return false;
            }

            intersection = new Intersection()
            {
                Shape = this
            };
            if (sqr == 0)
            {
                t = -b / 2;
                intersection.Point = ray[t];
            }
            else
            {
                sqr = (float)System.Math.Sqrt(sqr);
                t = System.Math.Min(-b + sqr, -b - sqr) * 0.5f;
                intersection.Point = ray[t];
            }
            intersection.Normal = new Normal(intersection.Point - this._center);
            GenerateTextureCoords(intersection);
            return true;
        }

        public void Transform(Transformation trans)
        {
            this._center = this._center.Transform(trans);
        }

        private void GenerateTextureCoords(Intersection intersection)
        {
            intersection.TextureCoordV = (float)System.Math.Acos(intersection.Normal.Z) / (float)System.Math.PI;
            intersection.TextureCoordU = (float)(System.Math.Atan2(intersection.Normal.Y, intersection.Normal.X) + System.Math.PI) / (float)(2 * System.Math.PI);
        }
    }
}
