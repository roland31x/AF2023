using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curs6_Poligoane
{
    public class Matrix
    {
        public static Random rng = new Random();
        public static Matrix Empty;
        int[,] A;
        int n { get { return A.GetLength(0); } }
        int m { get { return A.GetLength(1); } }
        public Matrix(int n = 0, int m = 0)
        {
            A = new int[n, m];
        }
        public Matrix(int n, int m, int min, int max)
        {
            A = new int[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    A[i, j] = rng.Next(min, max);
                }
            }
        }
        public Matrix(int n)
        {
            A = new int[n, n];
        }
        public static Matrix operator + (Matrix left, Matrix right)
        {           
            if(left.n != right.n || left.m != right.m)
            {
                return Empty;
            }
            Matrix result = new Matrix(left.n, right.m);
            for(int i = 0; i < left.n; i++)
            {
                for(int j = 0; j < left.m; j++)
                {
                    result.A[i,j] = left.A[i,j] + right.A[i,j];
                }
            }
            return result;
        }
        public static Matrix operator * (Matrix left, Matrix right)
        {
            if (left.m != right.n)
            {
                return Empty;
            }
            Matrix result = new Matrix(left.m, right.n);
            for(int i = 0; i < left.n; i++)
            {
                for(int j = 0; j < right.m; j++)
                {
                    result.A[i, j] = 0;
                    for(int k = 0; k < left.n; k++)
                    {
                        result.A[i,j] = left.A[i,k] * right.A[k,j];
                    }
                }
            }
            return result;
        }
        public List<string> View()
        {
            List<string> toReturn = new List<string>();
            string buffer;
            for(int i = 0; i < n; i++)
            {
                buffer = "";
                for(int j = 0; j < n; j++)
                {
                    buffer += A[i, j] + " ";
                }
                toReturn.Add(buffer);
            }
            return toReturn;
        }
    }
}
