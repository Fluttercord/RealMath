namespace RealMath.Polynomial
{
    public class FloatInterpolationChain : InterpolationChain<float>
    {
        public FloatInterpolationChain(int nodesRank, int segmentSize) 
            : base(nodesRank, segmentSize)
        {
        }

        protected override float[] Interpolate(Condition<float> condition, int segmentSize)
        {
            return PolynomialInterpolation.Interpolate(condition, segmentSize);
        }
    }
}
