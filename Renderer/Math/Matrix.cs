using System;

namespace Renderer.Math
{
    public class Matrix
    {
        private float[,] _elements = null;

        public float this[int y, int x]
        {
            get
            {
                return this._elements[y, x];
            }

            set
            {
                this._elements[y, x] = value;
            }
        }

        public static Matrix Identity()
        {
            return new Matrix(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1);
        }

        public Matrix()
            : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        public Matrix(Matrix copy)
        {
            this._elements = new float[,] {
                { copy._elements[0, 0], copy._elements[0, 1], copy._elements[0, 2] },
                { copy._elements[1, 0], copy._elements[1, 1], copy._elements[1, 2] },
                { copy._elements[2, 0], copy._elements[2, 1], copy._elements[2, 2] }
            };
        }

        public Matrix(float r0c0, float r0c1, float r0c2,
            float r1c0, float r1c1, float r1c2,
            float r2c0, float r2c1, float r2c2)
        {
            this._elements = new float[,] {
                { r0c0, r0c1, r0c2 },
                { r1c0, r1c1, r1c2 },
                { r2c0, r2c1, r2c2 }
            };
        }

        public Matrix Transpose()
        {
            return new Matrix(
                this._elements[0, 0], this._elements[1, 0], this._elements[2, 0],
                this._elements[0, 1], this._elements[1, 1], this._elements[2, 1],
                this._elements[0, 2], this._elements[1, 2], this._elements[2, 2]);
        }

        public Matrix Inverse()
        {
            return GuassJordan(this);
        }

        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            return new Matrix(
                lhs[0, 0] + rhs[0, 0], lhs[0, 1] + rhs[0, 1], lhs[0, 2] + rhs[0, 2],
                lhs[1, 0] + rhs[1, 0], lhs[1, 1] + rhs[1, 1], lhs[1, 2] + rhs[1, 2],
                lhs[2, 0] + rhs[2, 0], lhs[2, 1] + rhs[2, 1], lhs[2, 2] + rhs[2, 2]);
        }

        public static Matrix operator -(Matrix mat)
        {
            return new Matrix(
                -mat[0, 0], -mat[0, 1], -mat[0, 2],
                -mat[1, 0], -mat[1, 1], -mat[1, 2],
                -mat[2, 0], -mat[2, 1], -mat[2, 2]);
        }

        public static Matrix operator -(Matrix lhs, Matrix rhs)
        {
            return (lhs + -rhs);
        }

        public static Vector operator *(Matrix mat, Vector vec)
        {
            Vector newVec = new Vector();
            newVec.X = mat[0, 0] * vec.X + mat[0, 1] * vec.Y + mat[0, 2] * vec.Z;
            newVec.Y = mat[1, 0] * vec.X + mat[1, 1] * vec.Y + mat[1, 2] * vec.Z;
            newVec.Z = mat[2, 0] * vec.X + mat[2, 1] * vec.Y + mat[2, 2] * vec.Z;
            return newVec;
        }

        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            Matrix newMat = new Matrix();
            newMat[0, 0] = mat1[0, 0] * mat2[0, 0] + mat1[0, 1] * mat2[1, 0] + mat1[0, 2] * mat2[2, 0];
            newMat[1, 0] = mat1[1, 0] * mat2[0, 0] + mat1[1, 1] * mat2[1, 0] + mat1[1, 2] * mat2[2, 0];
            newMat[2, 0] = mat1[2, 0] * mat2[0, 0] + mat1[2, 1] * mat2[1, 0] + mat1[2, 2] * mat2[2, 0];

            newMat[0, 1] = mat1[0, 0] * mat2[0, 1] + mat1[0, 1] * mat2[1, 1] + mat1[0, 2] * mat2[2, 1];
            newMat[1, 1] = mat1[1, 0] * mat2[0, 1] + mat1[1, 1] * mat2[1, 1] + mat1[1, 2] * mat2[2, 1];
            newMat[2, 1] = mat1[2, 0] * mat2[0, 1] + mat1[2, 1] * mat2[1, 1] + mat1[2, 2] * mat2[2, 1];

            newMat[0, 2] = mat1[0, 0] * mat2[0, 2] + mat1[0, 1] * mat2[1, 2] + mat1[0, 2] * mat2[2, 2];
            newMat[1, 2] = mat1[1, 0] * mat2[0, 2] + mat1[1, 1] * mat2[1, 2] + mat1[1, 2] * mat2[2, 2];
            newMat[2, 2] = mat1[2, 0] * mat2[0, 2] + mat1[2, 1] * mat2[1, 2] + mat1[2, 2] * mat2[2, 2];
            return newMat;
        }

        public static Matrix operator *(Matrix mat, float scalar)
        {
            return new Matrix(
                mat[0, 0] * scalar, mat[0, 1] * scalar, mat[0, 2] * scalar,
                mat[1, 0] * scalar, mat[1, 1] * scalar, mat[1, 2] * scalar,
                mat[2, 0] * scalar, mat[2, 1] * scalar, mat[2, 2] * scalar);
        }
        
        public static Matrix operator *(float scalar, Matrix mat)
        {
            return (mat * scalar);
        }

        public static Matrix operator /(Matrix mat, float scalar)
        {
            return (mat * (1 / scalar));
        }

        private static Matrix GuassJordan(Matrix matrix)
        {
            Matrix copyMat = new Matrix(matrix);
            Matrix result = Matrix.Identity();
            int[] rows = new int[3];
            for (int i = 0; i < 3; ++i)
            {
                // Find the next pivot row.
                rows[i] = FindPivotRow(copyMat, i, rows);
                NormalizePivotRow(copyMat, result, rows[i], i);
                TransformRows(copyMat, result, rows[i], i);
            }
            ReorderRows(result, rows);
            return result;
        }

        private static void Swap<T>(ref T v1, ref T v2)
        {
            T temp = v1;
            v1 = v2;
            v2 = temp;
        }

        private static void SwapRows(Matrix matrix, int r1, int r2)
        {
            for (int i = 0; i < 3; ++i)
            {
                float temp = matrix[r1, i];
                matrix[r1, i] = matrix[r2, i];
                matrix[r2, i] = temp;
            }
        }

        private static void ReorderRows(Matrix matrix, int[] rows)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (i == rows[i])
                {
                    continue;
                }
                SwapRows(matrix, i, rows[i]);
                Swap(ref rows[i], ref rows[rows[i]]);
                --i;
            }
        }

        private static void TransformRows(Matrix matrix, Matrix result, int pivotRow, int pivotCol)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (i == pivotRow)
                {
                    continue;
                }
                float scale = matrix[i, pivotCol];
                for (int j = 0; j < 3; ++j)
                {
                    matrix[i, j] -= (scale * matrix[pivotRow, j]);
                    result[i, j] -= (scale * result[pivotRow, j]);
                }
            }
        }

        private static void NormalizePivotRow(Matrix matrix, Matrix result, int pivotRow, int pivotCol)
        {
            float pivotValue = matrix[pivotRow, pivotCol];
            for (int i = 0; i < 3; ++i)
            {
                matrix[pivotRow, i] /= pivotValue;
                result[pivotRow, i] /= pivotValue;
            }
        }

        private static int FindPivotRow(Matrix matrix, int pivotCol, int[] rows)
        {
            float max = 0;
            int index = -1;
            for (int i = 0; i < 3; ++i)
            {
                if (RowPivoted(rows, pivotCol, i))
                {
                    continue;
                }
                if (matrix[i, pivotCol] > max)
                {
                    max = matrix[i, pivotCol];
                    index = i;
                }
            }
            if (Math.Utils.IsZero(max))
            {
                throw new Exception("Matrix not invertible");
            }
            return index;
        }

        private static bool RowPivoted(int[] rows, int pivotCol, int row)
        {
            for (int i = 0; i < pivotCol; ++i)
            {
                if (row == rows[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
