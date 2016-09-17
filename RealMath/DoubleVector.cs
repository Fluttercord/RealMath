using System;

namespace RealMath
{
    public partial struct DoubleVector : IEquatable<DoubleVector>
    {
        private readonly double[] _elements;

        /// <summary>
        /// Возвращает или задает координату вектора
        /// </summary>
        /// <param name="i">Индекс координаты</param>
        /// <returns></returns>
        public double this[int i] { get { return _elements[i]; } set { _elements[i] = value; } }

        /// <summary>
        /// Возвращает размерность вектора
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Создает экземпляр заданной размерности. Все координаты будут равны нулю
        /// </summary>
        /// <param name="size">Число измерений</param>
        public DoubleVector(int size)
        {
            if (size < 0)
                throw new ArgumentException("size");
            Size = size;
            _elements = new double[Size];
        }

        /// <summary>
        /// Создает экземпляр по заданному вектору
        /// </summary>
        /// <param name="sample">Шаблон</param>
        public DoubleVector(DoubleVector sample)
            : this(sample.Size)
        {
            for (int i = 0; i < sample.Size; i++)
                _elements[i] = sample[i];
        }

        /// <summary>
        /// Создает экземпляр по массиву координат размерности длины массива.
        /// </summary>
        /// <param name="elements">Массив координат</param>
        public DoubleVector(params double[] elements)
        {
            int size = elements.Length;
            if (size <= 0)
                throw new ArgumentException("size");
            Size = size;
            _elements = elements;
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий сумму векторов.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static DoubleVector operator +(DoubleVector a, DoubleVector b)
        {
            if (a.Size != b.Size)
                throw new InvalidOperationException();
            int resultSize = a.Size;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = a[i] + b[i];
            return new DoubleVector(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий разность векторов.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static DoubleVector operator -(DoubleVector a, DoubleVector b)
        {
            if (a.Size != b.Size)
                throw new InvalidOperationException();
            int resultSize = a.Size;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = a[i] - b[i];
            return new DoubleVector(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий вектор, увеличенный в заданное число раз.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static DoubleVector operator *(DoubleVector a, double k)
        {
            int resultSize = a.Size;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = a[i] * k;
            return new DoubleVector(x);
        }

        /// <summary>
        /// Возвращает новый экземпляр, представляющий вектор, уменьшенный в заданное число раз.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static DoubleVector operator /(DoubleVector a, double k)
        {
            int resultSize = a.Size;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = a[i] / k;
            return new DoubleVector(x);
        }

        /// <summary>
        /// Возвращает модуль вектора
        /// </summary>
        /// <returns></returns>
        public double GetModule()
        {
            double result = 0;
            for (int i = 0; i < Size; i++)
                result += _elements[i] * _elements[i];
            return Math.Sqrt(result);
        }

        /// <summary>
        /// Возвращает новый экземпляр, размерность которого на единицу меньше, исключая координату по заданному индексу.
        /// </summary>
        /// <param name="index">Индекс исключаемой координаты</param>
        /// <returns></returns>
        public DoubleVector GetVectorWithExcluded(int index)
        {
            if ((index < 0) || (index >= Size))
                throw new InvalidOperationException();
            int resultSize = Size - 1;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
            {
                int offset = i >= index ? 1 : 0;
                x[i] = _elements[i + offset];
            }
            return new DoubleVector(x);
        }

        /// <summary>
        /// Поэлементно сравнивает с заданным вектором и возвращает true, если векторы равны.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DoubleVector other)
        {
            if (Size != other.Size)
                return false;
            for (int i = 0; i < Size; i++)
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_elements[i] != other[i])
                    return false;
            return true;
        }

        public void Reset(double resetValue)
        {
            for (int i = 0; i < Size; i++)
                _elements[i] = resetValue;
        }
    }
}