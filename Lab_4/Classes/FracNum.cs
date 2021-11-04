using System;
using System.Numerics;

namespace Lab_4.Classes
{
    class FracNum : INumber<FracNum>, IComparable<FracNum>
    {
        private BigInteger nom, denom;
        public FracNum(BigInteger nom, BigInteger denom)
        {
            this.nom = nom;
            this.denom = denom;
        }
        public FracNum(int nom, int denom)
        {
            this.nom = nom;
            this.denom = denom;
        }

        public FracNum Add(FracNum number) => new FracNum(
            nom * number.denom + denom * number.nom,
            denom * number.denom);
        public FracNum Subtract(FracNum number) => Add(new FracNum(-number.nom, number.denom));
        public FracNum Multiply(FracNum number)
        {
            var (new_nom, new_denom) = (nom * number.nom, denom * number.denom);
            if (new_denom == 0) throw new DivideByZeroException();
            return new FracNum(new_nom, new_denom);
        }
        public FracNum Divide(FracNum number) => Multiply(new FracNum(number.denom, number.nom));

        public (FracNum, FracNum) LeadToCommonDenominator(FracNum other) => (
            new FracNum(nom * other.denom, denom * other.denom), 
            new FracNum(other.nom * denom, other.denom * denom));
        public int CompareTo(FracNum other)
        {
            var (frac_1, frac_2) = LeadToCommonDenominator(other);
            return frac_1.denom.Sign * (frac_1.nom == frac_2.nom ? 0 : frac_1.nom > frac_2.nom ? 1 : -1);
        }
        public override string ToString() => $"{nom}/{denom}";

        // operators
        public static FracNum operator +(FracNum first, FracNum second) => first.Add(second);
        public static FracNum operator -(FracNum first, FracNum second) => first.Subtract(second);
        public static FracNum operator *(FracNum first, FracNum second) => first.Multiply(second);
        public static FracNum operator /(FracNum first, FracNum second) => first.Divide(second);

    }
}