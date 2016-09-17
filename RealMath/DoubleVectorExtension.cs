using System;
using System.Linq;

namespace RealMath
{
    public partial struct DoubleVector
    {
        /// <summary>
        /// Возвращает матрицу из одной строки, элементы которой являются координаты вектора
        /// </summary>
        /// <returns></returns>
        public DoubleMatrix GetAsMatrixRow()
        {
            double[,] x = new double[1, Size];
            for (int i = 0; i < Size; i++)
                x[0, i] = _elements[i];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает матрицу из однго столбца, элементы которого являются координаты вектора
        /// </summary>
        /// <returns></returns>
        public DoubleMatrix GetAsMatrixColumn()
        {
            double[,] x = new double[Size, 1];
            for (int i = 0; i < Size; i++)
                x[i, 0] = _elements[i];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает векторное произведение массива векторов
        /// </summary>
        /// <param name="operands">Массив векторов</param>
        /// <returns></returns>
        public static DoubleVector GetOrthogonale(DoubleVector[] operands)
        {
            if (operands.Length < 2)
                throw new InvalidOperationException();
            int resultSize = operands[0].Size;
            if (resultSize != operands.Length + 1)
                throw new InvalidOperationException();
            if (operands.Any(operand => operand.Size != resultSize))
                throw new InvalidOperationException();
            double[,] mx = new double[resultSize, resultSize];
            for (int i = 0; i < resultSize; i++)
                for (int j = 0; j < resultSize; j++)
                    mx[i, j] = i == 0 ? 1 : operands[i - 1][j];
            double[] vx = new double[resultSize];
            DoubleMatrix m = new DoubleMatrix(mx);
            for (int i = 0; i < resultSize; i++)
                vx[i] = m.GetAlgAddon(0, i);
            return new DoubleVector(vx);
        }

        public static double operator *(DoubleVector a, DoubleVector b)
        {
            return (a.GetAsMatrixRow() * b.GetAsMatrixColumn())[0, 0];
        }
    }
}