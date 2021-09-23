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
        public int Height { get => matrix.GetLength(0); }
        public int Width { get => matrix.GetLength(1); }
        public T this[int i, int j] { get => matrix[i, j]; }

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
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(matrix[i, j] + "\t");
                }
                if (i != Height - 1) sb.Append("\n");
            }
            return sb.ToString();
        }

        public T[,] GetTransponed()
        {
            var transponed = new T[Width, Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    transponed[j, i] = matrix[i, j];
                }
            }
            return transponed;
        }
        public MyMatrix<T> GetTransopedCopy() => new MyMatrix<T>(GetTransponed());
        public void Transpone() => matrix = GetTransponed();

        private static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public double[,] GetDoubleMatrix()
        {
            if (!IsNumericType(typeof(T))) throw new Exception("Not numeric exeption!");

            double[,] num_matrix = new double[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    num_matrix[i, j] = double.Parse(matrix[i, j].ToString());
                }
            }
            return num_matrix;
        }
        public static MyMatrix<T> operator +(MyMatrix<T> matrix_1, MyMatrix<T> matrix_2)
        {
            if (matrix_1.Height != matrix_2.Height || matrix_1.Width != matrix_2.Width)
            {
                throw new Exception("Sizes are not equal!");
            }
                
            double[,] numeric_matrix_1 = matrix_1.GetDoubleMatrix();
            double[,] numeric_matrix_2 = matrix_2.GetDoubleMatrix();
            T[,] output = new T[matrix_1.Height, matrix_1.Width];

            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    double num = numeric_matrix_1[i, j] + numeric_matrix_2[i, j];
                    try
                    {
                        output[i, j] = (T)Convert.ChangeType(num, typeof(T));
                    }
                    catch (Exception)
                    {
                        throw new Exception("Type cast error!");
                    }
                }
            }
            return new MyMatrix<T>(output);
        }
        public static MyMatrix<T> operator *(MyMatrix<T> matrix_1, MyMatrix<T> matrix_2)
        {
            if (matrix_1.Height != matrix_2.Width)
            {
                throw new Exception("Height & width are not equal!");
            }

            double[,] A = matrix_1.GetDoubleMatrix();
            double[,] B = matrix_2.GetDoubleMatrix();
            T[,] output = new T[matrix_1.Height, matrix_2.Width];

            for (int i = 0; i < matrix_1.Height; i++)
            {
                for (int j = 0; j < matrix_2.Width; j++)
                {
                    double num = 0;
                    for (int k = 0; k < matrix_1.Width; k++)
                    {
                        num += A[i, k] * B[k, j];
                    }

                    try
                    {
                        output[i, j] = (T)Convert.ChangeType(num, typeof(T));
                    }
                    catch (Exception)
                    {
                        throw new Exception("Type cast error!");
                    }
                }
            }
            return new MyMatrix<T>(output);
        }
    }
}
