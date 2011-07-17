﻿using Renderer.Math;

namespace Renderer.Shapes
{
    public class SphereShape : IShape
    {
        private float _radius;
        private Point _center = new Point();

        public IBRDF BRDF { get; private set; }

        public SphereShape(float radius, IBRDF brdf)
        {
            this._radius = radius;
            this.BRDF = brdf;
        }

        public bool Intersect(Ray ray, out Intersection intersection)
        {
            float a = Vector.Dot(ray.Direction, ray.Direction);
            Vector omc = ray.Origin - this._center;
            float b = 2 * Vector.Dot(ray.Direction, omc);
            float c = Vector.Dot(omc, omc) - (this._radius * this._radius);

            float sqr = b * b - 4 * c;
            if (sqr < 0)
            {
                intersection = null;
                return false;
            }

            intersection = new Intersection()
            {
                Shape = this
            };
            if (sqr == 0)
            {
                intersection.Point = ray[-b/2];
            }
            else
            {
                sqr = (float)System.Math.Sqrt(sqr);
                float t = System.Math.Min(-b + sqr, -b - sqr) * 0.5f;
                intersection.Point = ray[t];
            }
            intersection.Normal = new Normal(intersection.Point - this._center);
            return true;
        }

        public void Transform(Transformation trans)
        {
            this._center = this._center.Transform(trans);
        }
    }
}