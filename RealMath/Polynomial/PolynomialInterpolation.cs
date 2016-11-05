using System;
using System.Collections.Generic;
using RealMath.FloatType;

namespace RealMath.Polynomial
{
    public static class PolynomialInterpolation
    {
        /// <summary>
        /// y = c0 + c1*x + c2*x^2 + c3*x^3 + ... + cn*x^n
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static float GetValue(Vector polynomial, float parameter)
        {
            float result = 0;
            for (int i = 0; i < polynomial.Size; i++)
                result += (float) Math.Pow(parameter, i)*polynomial[i];
            return result;
        }

        public static Vector CreatePolynomial(Condition<float> condition)
        {
            int size = condition.Rank*2;
            List<Vector> devs = PolynomialDerivatives.GetBaseDerivativesSet(size);
            Vector yVector = new Vector(size);
            Matrix matrix = new Matrix(size, size);
            for (int i = 0; i < size; i+=2)
            {
                int blockOffset = i/2;

                yVector[i] = condition.GetZeroValue(blockOffset);
                yVector[i + 1] = condition.GetUnitValue(blockOffset);

                matrix[i, blockOffset] = 1;
                Vector dev = devs[blockOffset];
                for (int j = 0; j < dev.Size; j++)
                    matrix[i + 1, j + blockOffset] = dev[j];
            }
            VectorTransformation transformation = new VectorTransformation(matrix);
            Vector result = transformation.TransformBack(yVector);
            return result;
        }

        /// <summary>
        /// Interpolates between 0 and 1
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="stepsCount"></param>
        /// <returns></returns>
        public static float[] Interpolate(Condition<float> condition, int stepsCount)
        {
            Vector polynomial = CreatePolynomial(condition);
            float step = 1f/stepsCount;
            float[] result = new float[stepsCount];
            for (int i = 0; i < stepsCount; i++)
            {
                float parameter = step * i;
                result[i] = GetValue(polynomial, parameter);
            }
            return result;
        }
    }
}
