using System;

namespace RealMath.FloatType
{
    static class ArrayExtension
    {
        public static float GetDeterminant(this float[,] matrix, out float[] firstColumnAlgAddons)
        {
            float result = 0;
            int size = matrix.GetLength(0);
            if (size == 1)
            {
                firstColumnAlgAddons = null;
                return matrix[0, 0];
            }
            firstColumnAlgAddons = new float[size];
            for (int i = 0; i < size; i++)
            {
                float algAddon = matrix.GetAlgAddon(i, 0);
                firstColumnAlgAddons[i] = algAddon;
                result += matrix[i, 0] * algAddon;
            }
            return result;
        }

        public static float GetAlgAddon(this float[,] matrix, int i, int j)
        {
            return matrix.GetMinor(i, j) * ((i + j) % 2 == 0 ? 1 : -1);
        }

        public static float GetMinor(this float[,] matrix, int iIndex, int jIndex)
        {
            int rowsCount = matrix.GetLength(0) - 1;
            int columnsCount = matrix.GetLength(1) - 1;
            float[,] result = new float[rowsCount, columnsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                int iOffset = i >= iIndex ? 1 : 0;
                for (int j = 0; j < columnsCount; j++)
                {
                    int jOffset = j >= jIndex ? 1 : 0;
                    result[i, j] = matrix[i + iOffset, j + jOffset];
                }
            }
            float[] dummy;
            return result.GetDeterminant(out dummy);
        }

        public static float[,] GetAlgAddonMatrix(this float[,] matrix, float[] firstColumnAglAddons)
        {
            int size = matrix.GetLength(0);
            float[,] result = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = j == 0 ? firstColumnAglAddons[i] : matrix.GetAlgAddon(i, j);
            return result;
        }

        public static float[,] GetTransposed(this float[,] matrix)
        {
            int size = matrix.GetLength(0);
            float[,] result = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = matrix[j, i];
            return result;
        }

        public static float[,] GetInverted(this float[,] matrix, float determinant, float[] firstColumnAglAddons)
        {
            int size = matrix.GetLength(0);
            float[,] result = matrix.GetAlgAddonMatrix(firstColumnAglAddons).GetTransposed();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] /= determinant;
            return result;
        }
    }
}