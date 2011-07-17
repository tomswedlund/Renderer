using System;

namespace Renderer.Math
{
    public class Vector : IVector
    {
        public static float Dot(IVector v1, IVector v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector()
            : this(0, 0, 0)
        {
        }

        public Vector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector(Vector copy)
            : this(copy.X, copy.Y, copy.Z)
        {
        }

        public Normal Normalize()
        {
            return new Normal(this.X, this.Y, this.Z);
        }

        public float Magnitude()
        {
            return (float)System.Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
        }

        public Vector Trasnform(Transformation trans)
        {
            Vector tempVec = new Vector(this);
            tempVec = trans.Matrix * tempVec;
            return tempVec;
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vector operator -(Vector vec)
        {
            return new Vector(-vec.X, -vec.Y, -vec.Z);
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            return (lhs + -rhs);
        }

        public static Vector operator *(Vector vec, float scalar)
        {
            return new Vector(vec.X * scalar, vec.Y * scalar, vec.Z * scalar);
        }

        public static Vector operator /(Vector vec, float scalar)
        {
            return (vec * (1 / scalar));
        }
    }
}
