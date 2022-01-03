namespace Addition_Task_1.Classes
{
    class Rectangle : SteelPlate
    {
        public double width { get; private set; }
        public double height { get; private set; }

        public Rectangle(double width, double height, double steel_thick, double steel_density) : base(steel_thick, steel_density)
        {
            this.width = width;
            this.height = height;
        }

        public override double GetArea() => width * height;
    }
}