using RealMath.FloatType;

namespace RealMath.Polynomial
{
    public static class VectorInterpolation
    {
        public static Vector[] Interpolate(Condition<Vector> condition, int stepsCount)
        {
            int size = condition.GetZeroValue(0).Size;

            float[][] interpolatedCoords = new float[size][];
            for (int i = 0; i < size; i++)
            {
                Condition<float> coordCondition = new Condition<float>();
                for (int j = 0; j < condition.Rank; j++)
                    coordCondition.AddDerivativeValues(condition.GetZeroValue(j)[i], condition.GetUnitValue(j)[i]);
                float[] interpolatedCoord = PolynomialInterpolation.Interpolate(coordCondition, stepsCount);
                interpolatedCoords[i] = interpolatedCoord;
            }

            Vector[] result = new Vector[stepsCount];
            for (int i = 0; i < stepsCount; i++)
            {
                Vector vector = new Vector(size);
                for (int j = 0; j < size; j++)
                    vector[j] = interpolatedCoords[j][i];
                result[i] = vector;
            }

            return result;
        }
    }
}
