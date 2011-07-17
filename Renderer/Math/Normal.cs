namespace Renderer.Math
{
    public class Normal : IVector
    {
        private Vector _vector = null;

        public float X { get { return this._vector.X; } }
        public float Y { get { return this._vector.Y; } }
        public float Z { get { return this._vector.Z; } }

        public Normal(float x, float y, float z)
            : this(new Vector(x, y, z))
        {
        }

        public Normal(Normal copy)
        {
            this._vector = new Vector(copy._vector);
        }

        public Normal(Vector vec)
        {
            this._vector = new Vector(vec);
            this._vector /= this._vector.Magnitude();
        }

        public Normal Transform(Transformation trans)
        {
            Normal tempNorm = new Normal(this);
            tempNorm._vector = trans.Matrix.Transpose().Inverse() * tempNorm._vector;
            return tempNorm;
        }

        public static Normal operator -(Normal norm)
        {
            Normal newNorm = new Normal(norm);
            newNorm._vector = -newNorm._vector;
            return newNorm;
        }

        public static Point operator +(Normal norm, Point point)
        {
            Point newPoint = new Point(point);
            newPoint += norm._vector;
            return newPoint;
        }

        public static Point operator +(Point point, Normal norm)
        {
            return (norm + point);
        }

        public static Vector operator *(Normal norm, float scalar)
        {
            return (norm._vector * scalar);
        }

        public static Vector operator *(float scalar, Normal norm)
        {
            return (norm * scalar);
        }
    }
}
