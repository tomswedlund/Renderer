using Renderer.Math;

namespace Renderer.Shapes
{
    public class PlaneShape : IShape
    {
        private Point[] _corners = new Point[4];

        public IBRDF BRDF { get; private set; }

        public PlaneShape(IBRDF brdf)
        {
            this.BRDF = brdf;
            this.SetupCorners();
        }

        public bool Intersect(Ray ray, out Intersection intersection)
        {
            intersection = null;
            Vector pmo = this._corners[0] - ray.Origin;
            Normal normal = this.CalcPlaneNormal();
            float denom = Vector.Dot(normal, ray.Direction);
            if (!Math.Utils.IsZero(denom))
            {
                Point point = ray[Vector.Dot(normal, pmo) / denom];
                if (this.PointInRect(point))
                {
                    intersection = new Intersection()
                    {
                        Point = point,
                        Normal = normal,
                        Shape = this
                    };
                    return true;
                }
            }
            return false;
        }

        public void Transform(Transformation trans)
        {
            for (int i = 0; i < this._corners.Length; ++i)
            {
                this._corners[i] = this._corners[i].Transform(trans);
            }
        }

        #region Private methods
        private void SetupCorners()
        {
            this._corners[0] = new Point(-1, 1, 0);
            this._corners[1] = new Point(1, 1, 0);
            this._corners[2] = new Point(1, -1, 0);
            this._corners[3] = new Point(-1, -1, 0);
        }

        private Normal CalcPlaneNormal()
        {
            Vector arm1 = this._corners[1] - this._corners[0];
            Vector arm2 = this._corners[3] - this._corners[0];
            return new Normal(Vector.Cross(arm1, arm2));
        }

        private bool PointInRect(Point point)
        {
            Vector v1 = null;
            Vector v2 = null;
            for (int i = 0; i < 3; ++i)
            {
                v1 = this._corners[i + 1] - this._corners[i];
                v2 = point - this._corners[i];
                if (Vector.Dot(v1, v2) < 0)
                {
                    return false;
                }
            }
            v1 = this._corners[0] - this._corners[3];
            v2 = point - this._corners[3];
            return (Vector.Dot(v1, v2) >= 0);
        }
        #endregion
    }
}
