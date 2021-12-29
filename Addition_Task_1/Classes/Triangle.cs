namespace Addition_Task_1.Classes
{
    class Triangle : SteelPlate
    {
        public double cathetus1 { get; private set; }
        public double cathetus2 { get; private set; }

        public Triangle(double cathetus1, double cathetus2, double steel_thick, double steel_density) : base(steel_thick, steel_density)
        {
            this.cathetus1 = cathetus1;
            this.cathetus2 = cathetus2;
        }

        public override double GetArea() => (cathetus1 * cathetus2) / 2;
    }
}