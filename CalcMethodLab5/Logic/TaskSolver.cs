using CalcMethodLab5.Logic.Derivatives;

namespace CalcMethodLab5.Logic
{
    class TaskSolver
    {
        private Func func { get; }
        private LoggerManager logger { get; }
        private double step { get; }
        private double wall_size { get; }
        public TaskSolver(double wall_size, double step, double y_count)
        {
            this.wall_size = wall_size;
            this.step = step;
            func = new Func($"({wall_size} - {y_count}*x) * x");
            logger = LoggerSingleton.GetLogger();
        }
        public double Solve()
        {
            logger.WriteLog("Start calculation!");
            double x = step;
            DerivativeCalculator first_der_calculator = new FirstDerivativeCalculator(func);
            DerivativeCalculator second_der_calculator = new SecondDerivativeCalculator(func);
            double prev_der_value, curr_der;
            curr_der = first_der_calculator.GetDerivative(x);
            logger.WriteLog($"Find I derivative from X: {x} -> {curr_der}");
            do
            {
                prev_der_value = curr_der;
                x += step;
                curr_der = first_der_calculator.GetDerivative(x);
                logger.WriteLog($"Find I derivative from X: {x} -> {curr_der} and comparing...");
                if (prev_der_value * curr_der <= 0)
                {
                    var second_der = second_der_calculator.GetDerivative(x);
                    logger.WriteLog($"Find II derivative from X: {x} -> {second_der}");
                    if (second_der < 0)
                    {
                        logger.WriteLog($"X: {x} was returned!");
                        logger.WriteLog("End calculation!");
                        return x;
                    }
                }
            }
            while (x < wall_size);
            logger.WriteLog("End calculation! Nothing founded!");
            return double.NaN;
        }
    }
}