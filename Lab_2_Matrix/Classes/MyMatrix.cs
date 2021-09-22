using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_Matrix.Classes
{
    class MyMatrix<T> : ICloneable where T : IConvertible
    {
        private T[,] matrix;
        public int Height { get => matrix.Length; }
        public int Width { get => matrix.GetLength(1); }


        public MyMatrix(T[,] matrix)
        {
            this.matrix = matrix;
        }

        public MyMatrix(T[][] matrix)
        {
            this.matrix = new T[matrix.Length, matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                var array = matrix[i];
                if (array.Length != this.matrix.GetLength(1)) throw new Exception("Wrong input data!");
                for (int j = 0; j < array.Length; j++)
                {
                    var item = array[j];
                    this.matrix[i, j] = item;
                }
            }
        }

        public MyMatrix(string[] text)
        {
            var data = text.Select(x => x.Replace('\t', ' ').Trim().Split()).ToArray();
            matrix = new T[data.Length, data[0].Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length != matrix.GetLength(1)) throw new Exception("Wrong input data!");
                for (int j = 0; j < data[i].Length; j++)
                {
                    matrix[i, j] = (T)Convert.ChangeType(data[i][j], typeof(T));
                }
            }
        }

        public MyMatrix(string text) : this(text.Split('\n')) { }

        public object Clone() => new MyMatrix<T>(matrix);
        
    }
}
