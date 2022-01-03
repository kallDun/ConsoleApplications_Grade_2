namespace Addition_Task_1.Classes
{
    abstract class SteelPlate
    {
        protected SteelPlate(double steel_thick, double steel_density)
        {
            this.steel_thick = steel_thick;
            this.steel_density = steel_density;
        }

        public double steel_thick { get; private set; }
        public double steel_density { get; private set; }

        public abstract double GetArea();
        public double GetWeight() => steel_density * steel_thick * GetArea();

        public virtual string GetInfo() => $"THICK: {steel_thick}, DENSITY: {steel_density}, AREA: {GetArea()}, WEIGHT: {GetWeight()}";
    }
}