using System.Collections.Generic;
using System.Linq;
using RealMath.FloatType;

namespace RealMath.Polynomial
{
    public static class PolynomialDerivatives
    {
        private static readonly Dictionary<int, List<Vector>> Derivatives = new Dictionary<int, List<Vector>>();

        public static List<Vector> GetBaseDerivativesSet(int size)
        {
            List<Vector> set;
            if (Derivatives.TryGetValue(size, out set))
                return set.Select(v => new Vector(v)).ToList();
            Vector basePoly = new Vector(size);
            basePoly.Reset(1);
            set = GetAllDerivatives(basePoly);
            set.Insert(0, basePoly);
            Derivatives.Add(size, set);
            return set.Select(v => new Vector(v)).ToList();
        }

        /// <summary>
        /// y = c0 + c1*x + c2*x^2 + c3*x^3 + ... + cn*x^n
        /// y' = c1 + c2*2*x + c3*3*x^2 + ... + cn*n*x^(n - 1)
        /// </summary>
        /// <param name="polynomial"></param>
        /// <returns></returns>
        public static Vector GetDerivative(Vector polynomial)
        {
            Vector result = new Vector(polynomial.Size - 1);
            for (int i = 1; i < polynomial.Size; i++)
                result[i - 1] = polynomial[i] * i;
            return result;
        }

        public static List<Vector> GetAllDerivatives(Vector polynomial)
        {
            List<Vector> result = new List<Vector>();
            int count = polynomial.Size - 1;
            Vector iterator = polynomial;
            for (int i = 0; i < count; i++)
            {
                iterator = GetDerivative(iterator);
                result.Add(iterator);
            }
            return result;
        }
    }
}
