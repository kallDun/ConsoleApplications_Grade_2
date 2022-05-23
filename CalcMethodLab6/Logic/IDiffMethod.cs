namespace CalcMethodLab6.Logic
{
    interface IDiffMethod
    {
        ValuePair[] CalcDiffOn(CustomFunc func, double step, double range_from, double range_to, double init_value);
    }
}