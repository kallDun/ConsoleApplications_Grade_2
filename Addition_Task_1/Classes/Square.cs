namespace Addition_Task_1.Classes
{
    class Square : SteelPlate
    {
        public double width { get; private set; }

        public Square(double width, double steel_thick, double steel_density) : base(steel_thick, steel_density)
        {
            this.width = width;
        }

        public override double GetArea() => width * width;

        public override string GetInfo()
        {
            return base.GetInfo() + $", WIDTH: {width}";
        }
    }
}