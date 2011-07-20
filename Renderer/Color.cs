using Renderer.Math;

namespace Renderer
{
    public class Color
    {
        private Vector _vector = null;

        public float R
        {
            get { return this._vector.X; }
            set { this._vector.X = value; }
        }

        public float G
        {
            get { return this._vector.Y; }
            set { this._vector.Y = value; }
        }

        public float B
        {
            get { return this._vector.Z; }
            set { this._vector.Z = value; }
        }

        public Color()
            : this(0, 0, 0)
        {
        }

        public Color(float r, float g, float b)
        {
            this._vector = new Vector(r, g, b);
        }

        public Color(Color copy)
        {
            this._vector = new Vector(copy._vector);
        }

        public static Color operator +(Color c1, Color c2)
        {
            Color newColor = new Color();
            newColor._vector = c1._vector + c2._vector;
            return newColor;
        }

        public static Color operator *(Color c1, Color c2)
        {
            Color newColor = new Color()
            {
                R = c1.R * c2.R,
                G = c1.G * c2.G,
                B = c1.B * c2.B
            };
            return newColor;
        }

        public static Color operator *(Color color, float scalar)
        {
            Color newColor = new Color();
            newColor._vector = color._vector * scalar;
            return newColor;
        }

        public static Color operator *(float scalar, Color color)
        {
            return (color * scalar);
        }

        public static Color operator /(Color color, float scalar)
        {
            return (color * (1 / scalar));
        }
    }
}
