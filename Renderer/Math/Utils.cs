using System;

namespace Renderer.Math
{
    public static class Utils
    {
        private static readonly float ZERO_THRESH = 0.0001f;
        
        public static bool IsZero(float value)
        {
            return (value < ZERO_THRESH);
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
