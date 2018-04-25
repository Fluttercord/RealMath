using System;

namespace RyujiMath.FloatType
{
	public class VectorTransformation
	{
		private readonly Matrix _forward;
		private readonly Matrix _backward;

		public VectorTransformation(Matrix forward)
		{
			if (forward.RowsCount != forward.ColumnsCount)
				throw new InvalidOperationException();
			float det = forward.GetDeterminant();
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (det == 0)
				throw new InvalidOperationException();
			_forward = forward;
			_backward = forward.GetInverted();
		}

		private static Vector ToVector(Matrix matrix)
		{
			Vector result = new Vector(matrix.RowsCount);
			for (int i = 0; i < matrix.RowsCount; i++)
				result[i] = matrix[i, 0];
			return result;
		}

		private static Matrix ToMatrix(Vector vector)
		{
			Matrix result = new Matrix(vector.Size, 1);
			for (int i = 0; i < vector.Size; i++)
				result[i, 0] = vector[i];
			return result;
		}

		public Vector TransformForward(Vector vector)
		{
			if (vector.Size != _forward.RowsCount)
				throw new InvalidOperationException();
			return ToVector(_forward * ToMatrix(vector));
		}

		public Vector TransformBack(Vector vector)
		{
			if (vector.Size != _forward.RowsCount)
				throw new InvalidOperationException();
			return ToVector(_backward * ToMatrix(vector));
		}
	}
}
