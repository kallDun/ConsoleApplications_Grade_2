namespace CalcMethodLab3.Logic
{
    class EquationGroup
    {
        public Equation EquationX { get; private set; }
        public Equation EquationY { get; private set; }
        public EquationGroup(string equationX, string equationY)
        {
            EquationX = new Equation(equationX);
            EquationY = new Equation(equationY);
        }
    }
}