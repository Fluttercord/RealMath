namespace RealMath.Polynomial
{
    public class Condition<T>
    {
        private readonly InterpolationNode<T> _zeroNode = new InterpolationNode<T>(); 
        private readonly InterpolationNode<T> _unitNode = new InterpolationNode<T>();

        public int Rank => _zeroNode.Rank;

        public Condition()
        {

        }

        public Condition(T zeroValue, T unitValue)
        {
            AddDerivativeValues(zeroValue, unitValue);
        }

        public void AddDerivativeValues(T zeroValue, T unitValue)
        {
            _zeroNode.AddValue(zeroValue);
            _unitNode.AddValue(unitValue);
        }

        public T GetZeroValue(int i)
        {
            return _zeroNode[i];
        }

        public T GetUnitValue(int i)
        {
            return _unitNode[i];
        }

        /// <summary>
        /// Array of zero values concats array of unit values
        /// </summary>
        /// <returns></returns>
        public T[] GetAllValues()
        {
            int size = _zeroNode.Rank + _unitNode.Rank;
            T[] result = new T[size];
            for (int i = 0; i < size; i++)
                result[i] = i < _zeroNode.Rank ? _zeroNode[i] : _unitNode[i - _zeroNode.Rank];
            return result;
        }
    }
}
