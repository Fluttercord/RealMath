namespace RealMath
{
    static class DoubleArrayExtension
    {
        public static double GetDeterminant(this double[,] matrix, out double[] firstColumnAlgAddons)
        {
            double result = 0;
            int size = matrix.GetLength(0);
            if (size == 1)
            {
                firstColumnAlgAddons = null;
                return matrix[0, 0];
            }
            firstColumnAlgAddons = new double[size];
            for (int i = 0; i < size; i++)
            {
                double algAddon = matrix.GetAlgAddon(i, 0);
                firstColumnAlgAddons[i] = algAddon;
                result += matrix[i, 0] * algAddon;
            }
            return result;
        }

        public static double GetAlgAddon(this double[,] matrix, int i, int j)
        {
            return matrix.GetMinor(i, j) * ((i + j) % 2 == 0 ? 1 : -1);
        }

        public static double GetMinor(this double[,] matrix, int iIndex, int jIndex)
        {
            int rowsCount = matrix.GetLength(0) - 1;
            int columnsCount = matrix.GetLength(1) - 1;
            double[,] result = new double[rowsCount, columnsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                int iOffset = i >= iIndex ? 1 : 0;
                for (int j = 0; j < columnsCount; j++)
                {
                    int jOffset = j >= jIndex ? 1 : 0;
                    result[i, j] = matrix[i + iOffset, j + jOffset];
                }
            }
            double[] dummy;
            return result.GetDeterminant(out dummy);
        }

        public static double[,] GetAlgAddonMatrix(this double[,] matrix, double[] firstColumnAglAddons)
        {
            int size = matrix.GetLength(0);
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = j == 0 ? firstColumnAglAddons[i] : matrix.GetAlgAddon(i, j);
            return result;
        }

        public static double[,] GetTransposed(this double[,] matrix)
        {
            int size = matrix.GetLength(0);
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = matrix[j, i];
            return result;
        }

        public static double[,] GetInverted(this double[,] matrix, double determinant, double[] firstColumnAglAddons)
        {
            int size = matrix.GetLength(0);
            double[,] result = matrix.GetAlgAddonMatrix(firstColumnAglAddons).GetTransposed();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] /= determinant;
            return result;
        }
    }
}