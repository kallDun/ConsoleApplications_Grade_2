using org.mariuszgromada.math.mxparser;

namespace CalcMethodLab3.Logic
{
    class EquationGroup
    {
        public double Min, Max;
        public Equation EquationX { get; private set; }
        public Equation EquationY { get; private set; }
        public EquationGroup(string equationX, string equationY, double min, double max)
        {
            Min = min;
            Max = max;
            Function f_x = new Function($"f(x, y) = {equationX}");
            Function f_y = new Function($"f(x, y) = {equationY}");
            EquationX = new Equation( 
                (x, y) => f_x.calculate(x, y),
                (x, y) =>
                {
                    var arg_x = new Argument($"x = {x}");
                    var arg_y = new Argument($"y = {y}");
                    var e = new Expression("der(f(x, y), x)", f_x, arg_x, arg_y);
                    return e.calculate();
                },
                y =>
                {
                    var arg = new Argument($"y = {y}");
                    var e = new Expression($"solve(f(x, y), x, {min}, {max})", f_x, arg);
                    return e.calculate();
                });
            EquationY = new Equation(
                (x, y) => f_y.calculate(x, y),
                (x, y) =>
                {
                    var arg_x = new Argument($"x = {x}");
                    var arg_y = new Argument($"y = {y}");
                    var e = new Expression("der(f(x, y), y)", f_y, arg_x, arg_y);
                    return e.calculate();
                },
                x =>
                {
                    var arg = new Argument($"x = {x}");
                    var e = new Expression($"solve(f(x, y), y, {min}, {max})", f_y, arg);
                    return e.calculate();
                });
        }
    }
}