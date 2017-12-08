using System;

namespace RealMath.FloatType
{
	public class Vector : IEquatable<Vector>
	{
		private readonly float[] _elements;

		/// <summary>
		/// Возвращает или задает координату вектора
		/// </summary>
		/// <param name="i">Индекс координаты</param>
		/// <returns></returns>
		public float this[int i] { get { return _elements[i]; } set { _elements[i] = value; } }

		/// <summary>
		/// Возвращает размерность вектора
		/// </summary>
		public int Size { get; }

		/// <summary>
		/// Создает экземпляр заданной размерности. Все координаты будут равны нулю
		/// </summary>
		/// <param name="size">Число измерений</param>
		public Vector(int size)
		{
			if (size < 0)
				throw new ArgumentException("size");
			Size = size;
			_elements = new float[Size];
		}

		/// <summary>
		/// Создает экземпляр по заданному вектору
		/// </summary>
		/// <param name="sample">Шаблон</param>
		public Vector(Vector sample)
			: this(sample.Size)
		{
			for (int i = 0; i < sample.Size; i++)
				_elements[i] = sample[i];
		}

		/// <summary>
		/// Создает экземпляр по массиву координат размерности длины массива.
		/// </summary>
		/// <param name="elements">Массив координат</param>
		public Vector(params float[] elements)
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
		public static Vector operator +(Vector a, Vector b)
		{
			if (a.Size != b.Size)
				throw new InvalidOperationException();
			int resultSize = a.Size;
			float[] x = new float[resultSize];
			for (int i = 0; i < resultSize; i++)
				x[i] = a[i] + b[i];
			return new Vector(x);
		}

		/// <summary>
		/// Возвращает новый экземпляр, представляющий разность векторов.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector operator -(Vector a, Vector b)
		{
			if (a.Size != b.Size)
				throw new InvalidOperationException();
			int resultSize = a.Size;
			float[] x = new float[resultSize];
			for (int i = 0; i < resultSize; i++)
				x[i] = a[i] - b[i];
			return new Vector(x);
		}

		/// <summary>
		/// Возвращает новый экземпляр, представляющий вектор, увеличенный в заданное число раз.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="k"></param>
		/// <returns></returns>
		public static Vector operator *(Vector a, float k)
		{
			int resultSize = a.Size;
			float[] x = new float[resultSize];
			for (int i = 0; i < resultSize; i++)
				x[i] = a[i] * k;
			return new Vector(x);
		}

		/// <summary>
		/// Возвращает новый экземпляр, представляющий вектор, уменьшенный в заданное число раз.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="k"></param>
		/// <returns></returns>
		public static Vector operator /(Vector a, float k)
		{
			int resultSize = a.Size;
			float[] x = new float[resultSize];
			for (int i = 0; i < resultSize; i++)
				x[i] = a[i] / k;
			return new Vector(x);
		}

		/// <summary>
		/// Возвращает модуль вектора
		/// </summary>
		/// <returns></returns>
		public float GetModule()
		{
			float result = 0;
			for (int i = 0; i < Size; i++)
				result += _elements[i] * _elements[i];
			return (float)Math.Sqrt(result);
		}

		/// <summary>
		/// Поэлементно сравнивает с заданным вектором и возвращает true, если векторы равны.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Vector other)
		{
			if (Size != other.Size)
				return false;
			for (int i = 0; i < Size; i++)
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (_elements[i] != other[i])
					return false;
			return true;
		}

		public void Reset(float resetValue)
		{
			for (int i = 0; i < Size; i++)
				_elements[i] = resetValue;
		}
	}
}