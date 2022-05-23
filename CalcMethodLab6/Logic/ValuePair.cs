namespace CalcMethodLab6.Logic
{
    public class ValuePair
    {
        public double T, Y;
        public int index;
        public ValuePair(double t, double y, int index)
        {
            T = t;
            Y = y;
            this.index = index;
        }

        public override string ToString()
        {
            return $"[{T}, {Y}]";
        }
    }
}