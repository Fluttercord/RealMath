using System;

namespace RealMath
{
    public partial struct DoubleMatrix : IEquatable<DoubleMatrix>
    {
        private readonly double[,] _elements;
        private readonly bool _isQuadratic;
        private double _determinant;
        private bool _determinantMask;
        private double[] _firstColumnAlgAddons;

        /// <summary>
        /// Возвращает или задает значение элемента
        /// </summary>
        /// <param name="i">Строка элемента</param>
        /// <param name="j">Столбец элемента</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get { return _elements[i, j]; }
            set
            {
                _elements[i, j] = value;
                _determinantMask = true;
            }
        }

        /// <summary>
        /// Возвращает количество строк
        /// </summary>
        public int RowsCount { get; }

        /// <summary>
        /// Возвращает количество столбцов
        /// </summary>
        public int ColumnsCount { get; }

        /// <summary>
        /// Создает экземпляр с заданным количеством строк и столбцов. Значения всех элементов будут равны нулю
        /// </summary>
        /// <param name="rowsCount">Количество строк</param>
        /// <param name="columnsCount">Количество столбцов</param>
        public DoubleMatrix(int rowsCount, int columnsCount)
        {
            if (rowsCount <= 0)
                throw new ArgumentException("rowsCount");
            if (columnsCount <= 0)
                throw new ArgumentException("columnsCount");
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            _elements = new double[RowsCount, ColumnsCount];
            _isQuadratic = RowsCount == ColumnsCount;
            _determinant = 0;
            _determinantMask = true;
            _firstColumnAlgAddons = null;
        }

        /// <summary>
        /// Создает экземпляр по массиву элементов
        /// </summary>
        /// <param name="elements">Массив элементов</param>
        public DoubleMatrix(double[,] elements)
        {
            int rowsCount = elements.GetLength(0);
            if (rowsCount <= 0)
                throw new ArgumentException("rowsCount");
            int columnsCount = elements.GetLength(1);
            if (columnsCount <= 0)
                throw new ArgumentException("columnsCount");
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            _elements = elements;
            _isQuadratic = RowsCount == ColumnsCount;
            _determinant = 0;
            _determinantMask = true;
            _firstColumnAlgAddons = null;
        }

        /// <summary>
        /// Создает экземпляр по заданной матрице
        /// </summary>
        /// <param name="sample">Шаблон</param>
        public DoubleMatrix(DoubleMatrix sample)
            : this(sample.RowsCount, sample.ColumnsCount)
        {
            for (int i = 0; i < sample.RowsCount; i++)
                for (int j = 0; j < sample.ColumnsCount; j++)
                    _elements[i, j] = sample[i, j];
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий сумму матриц одинаковой размерности
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static DoubleMatrix operator +(DoubleMatrix a, DoubleMatrix b)
        {
            if (a.RowsCount != b.RowsCount)
                throw new InvalidOperationException();
            if (a.ColumnsCount != b.ColumnsCount)
                throw new InvalidOperationException();
            int resultRowsCount = a.RowsCount;
            int resultColumnsCount = a.ColumnsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    x[i, j] = a[i, j] + b[i, j];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий разность матриц одинаковой размерности
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static DoubleMatrix operator -(DoubleMatrix a, DoubleMatrix b)
        {
            if (a.RowsCount != b.RowsCount)
                throw new InvalidOperationException();
            if (a.ColumnsCount != b.ColumnsCount)
                throw new InvalidOperationException();
            int resultRowsCount = a.RowsCount;
            int resultColumnsCount = a.ColumnsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    x[i, j] = a[i, j] - b[i, j];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий произведение матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static DoubleMatrix operator *(DoubleMatrix a, DoubleMatrix b)
        {
            if (a.ColumnsCount != b.RowsCount)
                throw new InvalidOperationException();
            int resultRowsCount = a.RowsCount;
            int resultColumnsCount = b.ColumnsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    for (int k = 0; k < a.ColumnsCount; k++)
                        x[i, j] += a[i, k] * b[k, j];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий матрицу, элементы которой увеличены в заданное число раз
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static DoubleMatrix operator *(DoubleMatrix a, double k)
        {
            int resultRowsCount = a.RowsCount;
            int resultColumnsCount = a.ColumnsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    x[i, j] = a[i, j] * k;
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий матрицу, элементы которой уменьшены в заданное число раз
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static DoubleMatrix operator /(DoubleMatrix a, double k)
        {
            int resultRowsCount = a.RowsCount;
            int resultColumnsCount = a.ColumnsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    x[i, j] = a[i, j] / k;
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий минор элемента
        /// </summary>
        /// <param name="iIndex">Строка элемента</param>
        /// <param name="jIndex">Столбец элемента</param>
        /// <returns></returns>
        public double GetMinor(int iIndex, int jIndex)
        {
            if (!_isQuadratic)
                throw new InvalidOperationException("rowsCount != columnsCount");
            if (ColumnsCount + RowsCount == 2)
                throw new InvalidOperationException();
            if ((iIndex < 0) || (iIndex >= ColumnsCount))
                throw new InvalidOperationException();
            if ((jIndex < 0) || (jIndex >= RowsCount))
                throw new InvalidOperationException();
            return _elements.GetMinor(iIndex, jIndex);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий транспонированную матрицу
        /// </summary>
        /// <returns></returns>
        public DoubleMatrix GetTransposed()
        {
            int resultRowsCount = ColumnsCount;
            int resultColumnsCount = RowsCount;
            double[,] x = new double[resultRowsCount, resultColumnsCount];
            for (int i = 0; i < resultRowsCount; i++)
                for (int j = 0; j < resultColumnsCount; j++)
                    x[i, j] = _elements[j, i];
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает алгебраическое дополнение элемента
        /// </summary>
        /// <param name="iIndex">Строка элемента</param>
        /// <param name="jIndex">Столбец элемента</param>
        /// <returns></returns>
        public double GetAlgAddon(int iIndex, int jIndex)
        {
            if (!_isQuadratic)
                throw new InvalidOperationException("rowsCount != columnsCount");
            if (ColumnsCount + RowsCount == 2)
                throw new InvalidOperationException();
            if ((iIndex < 0) || (iIndex >= ColumnsCount))
                throw new InvalidOperationException();
            if ((jIndex < 0) || (jIndex >= RowsCount))
                throw new InvalidOperationException();
            if (!_determinantMask && jIndex == 0)
                return _firstColumnAlgAddons[iIndex];
            return _elements.GetAlgAddon(iIndex, jIndex);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий матрицу алгебраических дополнений
        /// </summary>
        /// <returns></returns>
        public DoubleMatrix GetAlgAddonMatrix()
        {
            if (!_isQuadratic)
                throw new InvalidOperationException("rowsCount != columnsCount");
            if (ColumnsCount + RowsCount == 2)
                throw new InvalidOperationException();
            double[,] x = new double[RowsCount, ColumnsCount];
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    x[i, j] = _elements.GetAlgAddon(i, j);
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий обратную матрицу
        /// </summary>
        /// <returns></returns>
        public DoubleMatrix GetInverted()
        {
            if (!_isQuadratic)
                throw new InvalidOperationException("rowsCount != columnsCount");
            if (_determinantMask)
            {
                _determinant = _elements.GetDeterminant(out _firstColumnAlgAddons);
                _determinantMask = false;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_determinant == 0d)
                throw new InvalidOperationException();
            return new DoubleMatrix(_elements.GetInverted(_determinant, _firstColumnAlgAddons));
        }

        /// <summary>
        /// Возвращает определитель
        /// </summary>
        /// <returns></returns>
        public double GetDeterminant()
        {
            if (!_isQuadratic)
                throw new InvalidOperationException("rowsCount != columnsCount");
            if (!_determinantMask) return _determinant;
            _determinant = _elements.GetDeterminant(out _firstColumnAlgAddons);
            _determinantMask = false;
            return _determinant;
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий единичную матрицу заданного размера
        /// </summary>
        /// <param name="size">Размер</param>
        /// <returns></returns>
        public static DoubleMatrix GetUnit(int size)
        {
            double[,] x = new double[size, size];
            for (int i = 0; i < size; i++)
                x[i, i] = 1;
            return new DoubleMatrix(x);
        }

        /// <summary>
        /// Поэлементно сравнивает с заданной матрицей и возвращает true, если матрицы равны.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DoubleMatrix other)
        {
            if (RowsCount != other.RowsCount)
                return false;
            if (ColumnsCount != other.ColumnsCount)
                return false;
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (_elements[i, j] != other[i, j])
                        return false;
            return true;
        }
    }
}