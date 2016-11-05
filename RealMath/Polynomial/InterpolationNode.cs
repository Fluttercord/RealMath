using System.Collections.Generic;

namespace RealMath.Polynomial
{
    public class InterpolationNode<T>
    {
        private readonly List<T> _values = new List<T>();

        public T this[int i] => _values[i];

        public int Rank => _values.Count;

        public void AddValue(T value)
        {
            _values.Add(value);
        }
    }
}
