using System;
using System.Linq;

namespace RealMath.FloatType
{
    public partial struct Vector
    {
        /// <summary>
        /// Возвращает матрицу из одной строки, элементы которой являются координаты вектора
        /// </summary>
        /// <returns></returns>
        public Matrix GetAsMatrixRow()
        {
            float[,] x = new float[1, Size];
            for (int i = 0; i < Size; i++)
                x[0, i] = _elements[i];
            return new Matrix(x);
        }

        /// <summary>
        /// Возвращает матрицу из однго столбца, элементы которого являются координаты вектора
        /// </summary>
        /// <returns></returns>
        public Matrix GetAsMatrixColumn()
        {
            float[,] x = new float[Size, 1];
            for (int i = 0; i < Size; i++)
                x[i, 0] = _elements[i];
            return new Matrix(x);
        }

        /// <summary>
        /// Возвращает векторное произведение массива векторов
        /// </summary>
        /// <param name="operands">Массив векторов</param>
        /// <returns></returns>
        public static Vector GetOrthogonale(Vector[] operands)
        {
            if (operands.Length < 2)
                throw new InvalidOperationException();
            int resultSize = operands[0].Size;
            if (resultSize != operands.Length + 1)
                throw new InvalidOperationException();
            if (operands.Any(operand => operand.Size != resultSize))
                throw new InvalidOperationException();
            float[,] mx = new float[resultSize, resultSize];
            for (int i = 0; i < resultSize; i++)
                for (int j = 0; j < resultSize; j++)
                    mx[i, j] = i == 0 ? 1 : operands[i - 1][j];
            float[] vx = new float[resultSize];
            Matrix m = new Matrix(mx);
            for (int i = 0; i < resultSize; i++)
                vx[i] = m.GetAlgAddon(0, i);
            return new Vector(vx);
        }

        public static float operator *(Vector a, Vector b)
        {
            return (a.GetAsMatrixRow() * b.GetAsMatrixColumn())[0, 0];
        }
    }
}