using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Curs9_ConsoleApp
{
    public class MyStack<T> : TAD<T>
    {
        public MyStack() : base()
        {
           
        }
        public void Push(T value)
        {
            AddBase(value);
        }
        public T Pop()
        {
            return RemoveTop();
        }
        public T Peek()
        {
            return values[Count - 1];
        }
    }
    public class MyQueue<T> : TAD<T>
    {
        public MyQueue() : base()
        {

        }
        public void Enqueue(T value)
        {
            AddBase(value);
        }
        public T Dequeue()
        {
            return RemoveStart();
        }
        public T Peek()
        {
            return values[0];
        }
    }
    public class MyOrderedList<T> : TAD<T> where T : IComparable<T>, IEquatable<T>
    {

        public MyOrderedList() : base()
        {

        }
        public T this[int index] => base[index];
        public void Add(T value)
        {           
            int i = Count;
            AddBase(value);
            while (i > 0 && values[i - 1].CompareTo(value) > 0)
            {
                (values[i], values[i - 1]) = (values[i - 1], values[i]);
                i--;
            }
        }
        public void Remove(T value)
        {
            RemoveBase(value);
        }
       
        public T RemoveAtIndex(int index)
        {
            return RemoveAtIndexBase(index);
        }
        public T RemoveHighest()
        {
            return RemoveTop();
        }
        public T RemoveLowest()
        {
            return RemoveStart();
        }
    }
    public abstract class TAD<T> 
    {
        protected T[] values;
        int _count;
        public int Count 
        { 
            get 
            { 
                return _count; 
            }
            set
            {
                _count = value;
                if(_count + 1 > Size)
                {
                    ResizeUp();
                }
                if(Size >= 20 && _count < Size / 4)
                {
                    ResizeDown();
                }
            }
        }

        protected int Size { get { return values.Length; } }
        public TAD()
        {
            values = new T[10];
            Count = 0;
        }
        protected T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return values[index];
            }
        }
        protected void ResizeDown()
        {
            T[] temp = new T[Size / 2];
            for (int i = 0; i < Count; i++)
            {
                temp[i] = values[i];
            }
            values = temp;
        }
        protected void AddBase(T value)
        {
            values[Count] = value;
            Count++;
        }
        protected void ResizeUp()
        {
            T[] temp = new T[Size * 2];
            for(int i = 0; i < Count; i++)
            {
                temp[i] = values[i];
            }
            values = temp;
        }
        protected T RemoveTop()
        {
            T tor = values[Count - 1];
            Count--;
            return tor;
        }
        protected T RemoveStart()
        {
            T tor = values[0];
            for (int i = 0; i < Count - 1; i++)
            {
                (values[i], values[i + 1]) = (values[i + 1], values[i]);
            }
            Count--;
            return tor;
        }
        protected T RemoveAtIndexBase(int index)
        {
            T tor = values[index];
            for (int i = index; i < Count - 1; i++)
            {
                (values[i], values[i + 1]) = (values[i + 1], values[i]);
            }
            Count--;
            return tor;
        }
        protected void RemoveBase(T value)
        {
            int idx = 0;
            for (int i = 0; i < Count; i++)
            {
                if (values[i].Equals(value))
                {
                    idx = i;
                    break;
                }
            }
            RemoveAtIndexBase(idx);
        }
    }
    public class RPN
    {
        string ToEval;
        Node Start = new Node();
        public RPN(string ToEvaluate)
        {
            ToEval = ToEvaluate;
        }
        public void Evaluate()
        {
            ToEval = ToEval.Replace(" ", "");
            
            Start.OpValue = ToEval[FindMinIndex()];

            // todo finishup
        }
        int FindMinIndex()
        {
            int minidx = -1;
            int minval = int.MaxValue;
            int val = 0;
            for (int i = 0; i < ToEval.Length; i++)
            {
                if (ToEval[i] == '(')
                {
                    val += 100;
                }
                else if (ToEval[i] == ')')
                {
                    val -= 100;
                }
                else if (ToEval[i] == '+' || ToEval[i] == '-')
                {
                    if (minval >= val + 1)
                    {
                        minval = val + 1;
                        minidx = i;
                    }
                }
                else if (ToEval[i] == '*' || ToEval[i] == '/')
                {
                    if (minval >= val + 2)
                    {
                        minval = val + 2;
                        minidx = i;
                    }
                }
            }
            return minidx;
        }
    }
    public class Node
    {
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public char OpValue { get; set; }
        public int IntVal { get; set; }
        public Node() 
        {

        }
    }
}
