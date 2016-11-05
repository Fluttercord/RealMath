using RealMath.FloatType;

namespace RealMath.Polynomial
{
    public class VectorInterpolationChain : InterpolationChain<Vector>
    {
        public VectorInterpolationChain(int nodesRank, int segmentSize) 
            : base(nodesRank, segmentSize)
        {
        }

        protected override Vector[] Interpolate(Condition<Vector> condition, int segmentSize)
        {
            return VectorInterpolation.Interpolate(condition, segmentSize);
        }
    }
}
