using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Curs9_FormsApp
{
    public class Complex
    {
        public static int MaxIterations = 500;
        public double Re { get; private set; }
        public double Im { get; private set; }
        public char Sgn
        {
            get
            {
                if (Im < 0)
                    return '-';
                else
                    return '+';
            }
        }

        public Complex(double real = 0, double imaginary = 0)
        {
            Re = real;
            Im = imaginary;
        }
        public static Complex operator !(Complex nr) => new Complex(nr.Re, nr.Im * -1);
        public static Complex operator +(Complex left, Complex right) => new Complex(left.Re + right.Re, left.Im + right.Im);
        public static Complex operator -(Complex left, Complex right) => new Complex(left.Re - right.Re, left.Im - right.Im);
        public static Complex operator *(Complex left, Complex right) => new Complex(left.Re * right.Re - left.Im * right.Im, right.Re * left.Im + left.Re * right.Im);
        public static Complex operator /(Complex left, Complex right)
        {
            Complex result = new Complex();
            Complex up = left * !right;
            Complex down = right * !right;
            result.Re = up.Re / down.Re;
            result.Im = up.Im / down.Im;
            return result;
        }
        public double Magnitude()
        {
            return Math.Sqrt(Re * Re + Im * Im);
        }
        public override string ToString()
        {
            return $"{Re} {Sgn} {Math.Abs(Im)}i";
        }
        //public Point ToPoint()
        //{
        //    //return new Point(Re, Im);
        //}
        public static int NumberOfIterations(int i, int j, int width, int height, double XZoomStart, double XZoomEnd, double YZoomStart, double YZoomEnd)
        {
            // 0 -> width => -2 -> 2
            // 0 -> height => -2 -> 2
            int n = 0;
            double x = (((double)i / (double)width) * (XZoomEnd - XZoomStart)) + XZoomStart;
            double y = (((double)j / (double)height) * (YZoomEnd - YZoomStart)) + YZoomStart;
            Complex C = new Complex(x, y);
            Complex Result = new Complex();
            do
            {
                Result = Result * Result + C;
                n++;
            }
            while (Result.Magnitude() < 4 && n < MaxIterations);
            return n;
        }
    }
}
