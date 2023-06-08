using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;
using System.Globalization;

namespace Curs9_ConsoleApp
{
    // tehnica de programare : Recursivitatea

    internal class Program
    {
        static void Main(string[] args)
        {
            ////Complex ToIterate = new Complex(0.2, -0.4);
            //ToIterate = ToIterate * ToIterate;
            //ToIterate = ToIterate * ToIterate;
            //ToIterate = ToIterate * ToIterate;
            //ToIterate = ToIterate * ToIterate;

            //Console.WriteLine(ToIterate);
            //MyDecimal mb =   new MyDecimal(32.231342425475m);
            //MyDecimal mb2 =  new MyDecimal(40.0499844235m);
            //MyDecimal t1 = new MyDecimal(0.0004323443985m);
            //MyDecimal t2 = new MyDecimal(-0.0055349856456m);
            ////BigDecimal a = new BigDecimal(new BigInteger(0), new BigInteger(242534));
            ////BigDecimal b = new BigDecimal(new BigInteger(0), new BigInteger(223145));
            ////BigDecimal c = b * a;
            ////Console.WriteLine(mb3);
            //// Console.WriteLine(mb.MultiplyByTen(2));

            ////int MaxIter = 500;
            ////MyDecimal x = t1;
            ////MyDecimal y = t2;
            ////int n = 0;
            ////do
            ////{
            ////    MyDecimal temp = x * x - y * y + t1;
            ////    y = MyDecimal.Two * x * y + t2;
            ////    x = temp;
            ////    n++;
            ////} while (x * x + y * y < MyDecimal.Four && n < MaxIter);

            //Console.WriteLine((t2).ToDecimal());
            //Console.WriteLine(t2 * MyDecimal.Two);
            MyOrderedList<int> myOrderedList = new MyOrderedList<int>();
            myOrderedList.Add(1);
            myOrderedList.Add(6);

            myOrderedList.Add(3);
            myOrderedList.Add(5);
            myOrderedList.RemoveAtIndex(2);
            myOrderedList.RemoveLowest();

            MyStack<int> mystack = new MyStack<int>();
            mystack.Push(1);
            mystack.Push(4);
            mystack.Push(5);
            mystack.Push(6);
            while (mystack.Count > 0)
            {
                Console.WriteLine(mystack.Pop());
            }
            MyQueue<int> myQueue = new MyQueue<int>();
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);

            myQueue.Enqueue(3);
            myQueue.Enqueue(4);
            myQueue.Enqueue(5);
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);

            myQueue.Enqueue(3);
            myQueue.Enqueue(4);
            myQueue.Enqueue(5);
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);

            myQueue.Enqueue(3);
            myQueue.Enqueue(4);
            myQueue.Enqueue(5);
            while (myQueue.Count > 0)
            {
                Console.WriteLine(myQueue.Dequeue());
            }
            //RPN myRPN = new RPN("(3-5)/(2*2-3)+1");
            //myRPN.Evaluate();
        }
        
    }
    public struct MyDecimal : IEquatable<MyDecimal>, IComparable<MyDecimal>
    {
        public static MyDecimal Four = new MyDecimal(4m);
        public static MyDecimal Two = new MyDecimal(2m);
        public static MyDecimal MinusOne = new MyDecimal(-1m);
        public static MyDecimal Zero = new MyDecimal(0m);

        public static int CurrentMaxPrecision = 50;

        bool isPositive;
        int intValue;
        int Sign { get { return Math.Sign(intValue); } }
        int[] fraction;
        int usedDecimals 
        { 
            get
            {  
                for(int i = CurrentSize - 1; i >= 0; i--)
                {
                    if (fraction[i] != 0)
                    {
                        return i + 1;
                    }
                }
                return 0;                
            } 
        } 
        int CurrentSize { get { return fraction.Length; } }
        public MyDecimal(decimal value)
        {
            if(value > 0)
            {
                isPositive = true;
            }
            else
            {
                isPositive = false;
            }
            intValue = (int)Math.Floor(Math.Abs(value));
            fraction = new int[CurrentMaxPrecision];
            
            StringBuilder sb = new StringBuilder();
            sb.Append(value);
            bool ok = false;
            int j = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                if (ok)
                {
                    fraction[j] = sb[i] - '0';
                    j++;
                }
                else if (!ok)
                {
                    if (sb[i] == '.')
                    {
                        ok = true;
                    }
                }
            }
        }
        public MyDecimal(int placeholder)
        {
            isPositive = true;
            intValue = 0;
            fraction = new int[CurrentMaxPrecision];
        }
        public static MyDecimal operator +(MyDecimal left, MyDecimal right)
        {
            if (!left.isPositive || !right.isPositive)
            {
                if (left.isPositive)
                {
                    return left - right.Absolute();
                }
                else if (right.isPositive)
                {
                    return right - left.Absolute();
                }
                else
                {
                    return (right.Absolute() + left.Absolute()).Negative();
                }
            }

            MyDecimal toReturn = new MyDecimal(Math.Max(left.CurrentSize, right.CurrentSize));
            int sizediff = left.CurrentSize - right.CurrentSize;
            if (sizediff > 0)
            {
                right.ExtendPrecision(sizediff);
            }
            else if (sizediff < 0)
            {
                left.ExtendPrecision(Math.Abs(sizediff));
            }
            toReturn.intValue = left.intValue + right.intValue;
            for (int i = Math.Max(left.usedDecimals, right.usedDecimals) - 1; i >= 1; i--)
            {
                toReturn.fraction[i] += left.fraction[i] + right.fraction[i];
                if (toReturn.fraction[i] >= 10)
                {
                    toReturn.fraction[i - 1] += 1;
                    toReturn.fraction[i] -= 10;
                }
            }
            toReturn.fraction[0] += left.fraction[0] + right.fraction[0];
            if (toReturn.fraction[0] >= 10)
            {
                toReturn.intValue += 1;
                toReturn.fraction[0] -= 10;
            }


            return toReturn;

        }
        public static MyDecimal operator -(MyDecimal left, MyDecimal right)
        {
            if (left.isPositive && !right.isPositive)
            {
                return left + right.Absolute();
            }
            if (!left.isPositive && right.isPositive)
            {
                return (left.Absolute() + right).Negative();
            }
            if (!left.isPositive && !right.isPositive)
            {
                if(left.Absolute() < right.Absolute())
                {
                    return right.Absolute() - left.Absolute();
                }
                else
                {
                    return (left.Absolute() - right.Absolute()).Negative();
                }
            }
            if(left.Absolute() < right.Absolute())
            {
                return (right.Absolute() - left.Absolute()).Negative();
            }

            MyDecimal toReturn = new MyDecimal(Math.Max(left.CurrentSize, right.CurrentSize));
            int sizediff = left.CurrentSize - right.CurrentSize;
            if (sizediff > 0)
            {
                right.ExtendPrecision(sizediff);
            }
            else if (sizediff < 0)
            {
                left.ExtendPrecision(Math.Abs(sizediff));
            }

            for (int i = Math.Max(left.usedDecimals, right.usedDecimals) - 1; i >= 1; i--)
            {
                toReturn.fraction[i] += left.fraction[i] - right.fraction[i];
                if (toReturn.fraction[i] < 0)
                {
                    toReturn.fraction[i - 1] -= 1;
                    toReturn.fraction[i] += 10;
                }
            }
            toReturn.fraction[0] += left.fraction[0] - right.fraction[0];
            if (toReturn.fraction[0] < 0)
            {
                toReturn.intValue -= 1;
                toReturn.fraction[0] += 10;
            }
            toReturn.intValue += left.intValue - right.intValue;
            if (toReturn.intValue < 0)
            {
                toReturn.isPositive = false;
                toReturn.intValue = Math.Abs(toReturn.intValue);
            }

            return toReturn;
        }
        public static MyDecimal operator *(MyDecimal left, MyDecimal right)
        {
            if(left.intValue * right.intValue > 1000000)
            {
                throw new InvalidOperationException("This struct is made for infinite precision towards zero!!");
            }
            bool ResultSign = true;
            if(!left.isPositive || !right.isPositive)
            {
                if(!(!left.isPositive && !right.isPositive))
                {
                    ResultSign = false;
                }
            }
            MyDecimal toReturn = new MyDecimal(left.CurrentSize + right.CurrentSize);
            int[] intvalues = new int[8];
            int[] leftint = new int[8];
            int[] rightint = new int[8];
            int leftaux = left.intValue;
            int rightaux = right.intValue;
            int helper = 7;
            while(leftaux > 0)
            {
                leftint[helper] = leftaux % 10;
                leftaux /= 10;
                helper--;
            }
            helper = 7;
            while (rightaux > 0)
            {
                rightint[helper] = rightaux % 10;
                rightaux /= 10;
                helper--;
            }


            for (int i = left.usedDecimals - 1; i >= 0; i--)
            {
                for(int j = right.usedDecimals - 1; j >= 0; j--)
                {
                    if(i + j + 1 >= CurrentMaxPrecision)
                    {
                        continue;
                    }
                    toReturn.fraction[i + j + 1] += left.fraction[i] * right.fraction[j];
                    if (toReturn.fraction[i + j + 1] >= 10)
                    {
                        toReturn.fraction[i + j] += toReturn.fraction[i + j + 1] / 10;
                                               
                        toReturn.fraction[i + j + 1] %= 10;
                    }
                }
                for(int j = rightint.Length - 1; j >= 0; j--)
                {
                    if((rightint.Length - 1 - j) - i - 1 < 0)
                    {
                        int fractionalindex = Math.Abs((rightint.Length - 1 - j) - i);
                        if(fractionalindex > CurrentMaxPrecision)
                        {
                            continue;
                        }
                        toReturn.fraction[fractionalindex] += left.fraction[i] * rightint[j];
                        if(toReturn.fraction[fractionalindex] >= 10)
                        {
                            if(fractionalindex == 0)
                            {
                                intvalues[intvalues.Length - 1] += toReturn.fraction[fractionalindex] / 10;
                            }
                            else
                            {
                                toReturn.fraction[fractionalindex - 1] += toReturn.fraction[fractionalindex] / 10;
                            }
                            toReturn.fraction[fractionalindex] %= 10;
                        }
                    }
                    else
                    {
                        intvalues[j] += left.fraction[i] * rightint[j];
                        if (intvalues[j] >= 10)
                        {
                            intvalues[j - 1] += intvalues[j] / 10;
                            intvalues[j] %= 10;
                        }
                    }                   
                    
                }
            }
            for(int i = leftint.Length - 1; i >= 0; i--)
            {
                for (int j = right.usedDecimals - 1; j >= 0; j--)
                {
                    if((leftint.Length - 1 - i) - j - 1 < 0)
                    {
                        int fractionalindex = Math.Abs((leftint.Length - 1 - i) - j);
                        if(fractionalindex > CurrentMaxPrecision)
                        {
                            continue;
                        }
                        toReturn.fraction[fractionalindex] += leftint[i] * right.fraction[j];
                        if (toReturn.fraction[fractionalindex] >= 10)
                        {
                            if (fractionalindex == 0)
                            {
                                intvalues[intvalues.Length - 1] += toReturn.fraction[fractionalindex] / 10;
                            }
                            else
                            {
                                toReturn.fraction[fractionalindex - 1] += toReturn.fraction[fractionalindex] / 10;
                            }
                            toReturn.fraction[fractionalindex] %= 10;
                        }
                    }
                    else
                    {
                        int intindex = Math.Abs((leftint.Length - 1 - i) - j - 1);
                        intvalues[intvalues.Length - 1 - intindex] += leftint[i] * right.fraction[j];
                        if (intvalues[intvalues.Length - 1 - intindex] >= 10)
                        {
                            intvalues[intvalues.Length - 1 - intindex - 1] += intvalues[intvalues.Length - 1 - intindex] / 10;
                            intvalues[intvalues.Length - 1 - intindex] %= 10;
                        }
                    }
                }
                for (int j = rightint.Length - 1; j >= 0; j--)
                {
                    int intindex = intvalues.Length - 1 - Math.Abs((leftint.Length - 1 - i - (rightint.Length - 1 - j)));
                    intvalues[intindex] += leftint[i] * rightint[j];
                    if (intvalues[intindex] >= 10)
                    {
                        intvalues[intindex - 1] += intvalues[intindex] / 10;
                        intvalues[intindex] %= 10;
                    }
                }
            }
            int newval = 0;
            for(int i = 0; i < intvalues.Length; i++)
            {
                newval += intvalues[intvalues.Length - 1 - i] * (int)Math.Pow(10, i);
            }
            toReturn.intValue = newval;
            toReturn.isPositive = ResultSign;
            return toReturn;
        }
        public static bool operator <(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator >(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <=(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) <= 0;
        }
        public static bool operator >=(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) >= 0;
        }
        public static bool operator ==(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) == 0;
        }
        public static bool operator !=(MyDecimal left, MyDecimal right)
        {
            return left.CompareTo(right) != 0;
        }
        MyDecimal Negative()
        {
            MyDecimal neg = Clone();
            neg.isPositive = false;
            return neg;
        }
        MyDecimal Absolute()
        {
            MyDecimal abs = Clone();
            abs.isPositive = true;
            return abs;
        }
        MyDecimal Clone()
        {
            MyDecimal toReturn = new MyDecimal(this.CurrentSize);
            for(int i = 0; i < usedDecimals; i++)
            {
                toReturn.fraction[i] = fraction[i];
            }
            toReturn.intValue = intValue;
            toReturn.isPositive = isPositive;
            return toReturn;
        }
        void ExtendPrecision(int AmountOfExtraDigits)
        {
            int size = CurrentSize + AmountOfExtraDigits;
            int[] newfrac = new int[50 * (size / 50 + 1)];
            for (int i = 0; i < usedDecimals; i++)
            {
                newfrac[i] = fraction[i];
            }
            fraction = newfrac;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!isPositive)
            {
                stringBuilder.Append('-');
            }
            stringBuilder.Append(intValue);
            stringBuilder.Append('.');
            for (int i = 0; i < usedDecimals; i++)
            {
                stringBuilder.Append(fraction[i]);
            }
            return stringBuilder.ToString();
        }

        public int CompareTo(MyDecimal other)
        {

            if (this.intValue < other.intValue)
            {
                return -1;
            }
            else if (this.intValue > other.intValue)
            {
                return 1;
            }
            else if (this.intValue == other.intValue)
            {
                for (int i = 0; i < Math.Min(usedDecimals, other.usedDecimals); i++)
                {
                    if (this.fraction[i] < other.fraction[i])
                    {
                        return -1;
                    }
                    else if (this.fraction[i] > other.fraction[i])
                    {
                        return 1;
                    }
                }
                if (this.usedDecimals < other.usedDecimals)
                {
                    return -1;
                }
                else if (this.usedDecimals > other.usedDecimals)
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool Equals(MyDecimal other)
        {
            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is MyDecimal)
            {
                return this.Equals(obj);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public decimal ToDecimal()
        {
            decimal toReturn = 0;
            toReturn += intValue;
            
            for (int i = 0; i < 28; i++)
            {
                toReturn += fraction[i] / (decimal)Math.Pow(10, i + 1);
            }
            if (!isPositive)
            {
                toReturn *= -1;
            }

            return toReturn;
        }
    }
    public struct BigDecimal : IEquatable<BigDecimal>, IComparable<BigDecimal>
    {
        public static BigDecimal Zero = new BigDecimal();
        public static BigDecimal Four = new BigDecimal(new BigInteger(4));
        public static BigDecimal Two = new BigDecimal(new BigInteger(2));

        public BigInteger IntegerPart { get; private set; }
        public BigInteger FractionalPart { get; private set; }

        public BigDecimal(BigInteger integerPart, BigInteger fractionalPart)
        {
            IntegerPart = integerPart;
            FractionalPart = fractionalPart;
        }

        public BigDecimal(BigInteger integerPart)
        {
            IntegerPart = integerPart;
            FractionalPart = BigInteger.Zero;
        }

        public BigDecimal(int i)
        {
            IntegerPart = BigInteger.Zero;
            FractionalPart = BigInteger.Zero;
        }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            BigInteger integerPart = left.IntegerPart + right.IntegerPart;
            BigInteger fractionalPart = left.FractionalPart + right.FractionalPart;

            // Check for carry-over from fractional part
            if (BigInteger.Abs(fractionalPart) >= BigInteger.Pow(10, Scale))
            {
                integerPart += BigInteger.DivRem(fractionalPart, BigInteger.Pow(10, Scale), out fractionalPart);
            }

            return new BigDecimal(integerPart, fractionalPart);
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            BigInteger integerPart = left.IntegerPart - right.IntegerPart;
            BigInteger fractionalPart = left.FractionalPart - right.FractionalPart;

            // Check for borrow from fractional part
            if (fractionalPart < 0)
            {
                integerPart--;
                fractionalPart += BigInteger.Pow(10, Scale);
            }

            return new BigDecimal(integerPart, fractionalPart);
        }

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            BigInteger integerPart = left.IntegerPart * right.IntegerPart;
            BigInteger fractionalPart = left.FractionalPart * right.FractionalPart;

            // Scale down fractional part
            fractionalPart /= BigInteger.Pow(10, Scale * 2);

            // Add overflow from integer part multiplication
            BigInteger overflow = (left.IntegerPart * right.FractionalPart) + (right.IntegerPart * left.FractionalPart);
            overflow /= BigInteger.Pow(10, Scale);

            return new BigDecimal(integerPart + overflow, fractionalPart);
        }

        public static BigDecimal operator /(BigDecimal dividend, BigDecimal divisor)
        {
            BigInteger scaledDividend = (dividend.IntegerPart * BigInteger.Pow(10, Scale)) + dividend.FractionalPart;
            BigInteger scaledDivisor = (divisor.IntegerPart * BigInteger.Pow(10, Scale)) + divisor.FractionalPart;

            BigInteger integerPart = BigInteger.DivRem(scaledDividend, scaledDivisor, out BigInteger fractionalPart);
            fractionalPart *= BigInteger.Pow(10, Scale);

            return new BigDecimal(integerPart, fractionalPart);
        }

        public static bool operator ==(BigDecimal left, BigDecimal right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BigDecimal left, BigDecimal right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(BigDecimal left, BigDecimal right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(BigDecimal left, BigDecimal right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(BigDecimal left, BigDecimal right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(BigDecimal left, BigDecimal right)
        {
            return left.CompareTo(right) >= 0;
        }

        public int CompareTo(BigDecimal other)
        {
            BigInteger scaledThis = (IntegerPart * BigInteger.Pow(10, Scale)) + FractionalPart;
            BigInteger scaledOther = (other.IntegerPart * BigInteger.Pow(10, Scale)) + other.FractionalPart;

            return scaledThis.CompareTo(scaledOther);
        }

        // Additional overloaded arithmetic operations can be implemented in a similar manner

        public override string ToString()
        {
            string decimalPart = FractionalPart.ToString().PadLeft(Scale, '0');
            return $"{IntegerPart}.{decimalPart}";
        }

        public bool Equals(BigDecimal other)
        {
            return this.CompareTo(other) == 0;
        }

        // Define the desired scale for the decimal representation
        private const int Scale = 10;
    }
}
