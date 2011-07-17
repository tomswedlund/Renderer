namespace Renderer.Math
{
    public class Point : IVector
    {
        private Vector _vector = null;

        public float X
        {
            get { return this._vector.X; }
            set { this._vector.X = value; }
        }

        public float Y
        {
            get { return this._vector.Y; }
            set { this._vector.Y = value; }
        }

        public float Z
        {
            get { return this._vector.Z; }
            set { this._vector.Z = value; }
        }

        public Point()
            : this(0, 0, 0)
        {
        }

        public Point(Point copy)
            : this(copy.X, copy.Y, copy.Z)
        {
        }

        public Point(float x, float y, float z)
        {
            this._vector = new Vector(x, y, z);
        }

        public Point Transform(Transformation trans)
        {
            Point tempPoint = new Point(this);
            tempPoint._vector = trans.Matrix * tempPoint._vector + trans.Vector;
            return tempPoint;
        }

        public static Point operator +(Vector vec, Point point)
        {
            Point newPoint = new Point(point);
            newPoint._vector += vec;
            return newPoint;
        }

        public static Point operator +(Point point, Vector vec)
        {
            return (vec + point);
        }

        public static Vector operator -(Point p1, Point p2)
        {
            return (p1._vector - p2._vector);
        }
    }
}
