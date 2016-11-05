using System;
using System.Collections.Generic;

namespace RealMath.Polynomial
{
    public abstract class InterpolationChain<T>
    {
        private readonly List<InterpolationNode<T>> _nodes = new List<InterpolationNode<T>>();

        public int NodesRank { get; }
        public int SegmentSize { get; }

        protected InterpolationChain(int nodesRank, int segmentSize)
        {
            if (nodesRank < 1)
                throw new ArgumentException();
            NodesRank = nodesRank;
            SegmentSize = segmentSize;
        }

        public void AddNode(InterpolationNode<T> node)
        {
            if (node.Rank != NodesRank)
                throw new ArgumentException();
            _nodes.Add(node);
        }

        public void AddValuesLayer(T[] layer)
        {
            if (layer.Length != _nodes.Count)
                throw new InvalidOperationException();
            int i = 0;
            foreach (InterpolationNode<T> node in _nodes)
            {
                if (node.Rank >= NodesRank)
                    throw new InvalidOperationException();
                node.AddValue(layer[i]);
                i++;
            }
        }

        public T[] CalcValues()
        {
            if (_nodes.Count < 2)
                throw new InvalidOperationException();
            List<T> resultList = new List<T>();
            for (int i = 0; i < _nodes.Count - 1; i++)
            {
                InterpolationNode<T> zeroValues = _nodes[i];
                InterpolationNode<T> unitValues = _nodes[i + 1];
                Condition<T> condition = new Condition<T>();
                for (int j = 0; j < NodesRank; j++)
                    condition.AddDerivativeValues(zeroValues[j], unitValues[j]);
                T[] segment = Interpolate(condition, SegmentSize);
                resultList.AddRange(segment);
            }
            return resultList.ToArray();
        }

        protected abstract T[] Interpolate(Condition<T> condition, int segmentSize);
    }
}
