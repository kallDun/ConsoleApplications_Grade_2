using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Classes
{
    public interface INumber<T> where T : INumber<T>
    {
        T Add(T number);
        T Subtract(T number);
        T Multiply(T number);
        T Divide(T number);
    }
}
