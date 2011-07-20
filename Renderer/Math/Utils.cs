using System;

namespace Renderer.Math
{
    public static class Utils
    {
        private static readonly float ZERO_THRESH = 0.0001f;
        private static readonly float PERTURB = 0.1f;
        
        public static bool IsZero(float value)
        {
            return (System.Math.Abs(value) < ZERO_THRESH);
        }

        public static bool LessThan(float v1, float v2)
        {
            return (v1 - v2) < -ZERO_THRESH;
        }

        public static Point Perturb(Point point, Normal toward)
        {
            return point + PERTURB * toward;
        }

        public static float Deg2Rad(float degrees)
        {
            return (degrees * (float)System.Math.PI / 180.0f);
        }

        public static void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
