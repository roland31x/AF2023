using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs14_ConsoleApp
{
    internal class Program
    {
        // Backtracking
        static void Main(string[] args)
        {
            //SaseZaruri();
            //Permutari(4);
            //Dame(4);
            //Aranjamente(4,3);
            Combinari(4, 2);
        }
        static void Permutari(int n)
        {
            int[] v = new int[n];
            int[] ints = new int[n + 1];
            BackSelect(v, ints, 0, n);
        }
        static void SaseZaruri()
        {
            int n = 6;
            int[] v = new int[n];
            Back(v, 0, n, 5);
        }
        static void Back(int[] v, int k, int n, int p)
        {
            // n - cate foruri
            // p - val max a forurilor
            // daca n = p => [1,n]^[1,n] - produs cartezian
            if(k >= n)
            {
                for(int i = 0; i < n; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for(int i = 1; i <= p; i++)
                {
                    v[k] = i;
                    Back(v, k + 1, n, p);
                }
            }            
        }
        static void BackSelect(int[] v, int[] selected, int k, int n)
        {
            // permutari
            if (k >= n)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (selected[i] == 0)
                    {
                        selected[i] = 1;
                        v[k] = i;
                        BackSelect(v, selected, k + 1, n);
                        selected[i] = 0;
                    }
                }
            }
        }
        // problema damelor, se dau N dame si trebuie introduse intr-o tabla de nxn fara sa se intersecteze.
        static void Dame(int n)
        {
            int[] v = new int[n];
            int[] sel = new int[n];
            BackSelectDame(v, sel, 0, n);
        }
        static void BackSelectDame(int[] v, int[] selected, int k, int n)
        {
            // permutari
            if (k >= n)
            {
                bool ok = true;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (Math.Abs(v[j] - v[i]) == Math.Abs(j - i))
                        {
                            ok = false;
                            break;
                        }
                    }
                }
                if (!ok)
                {
                    return;
                }
                for (int i = 0; i < n; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (selected[i] == 0)
                    {
                        selected[i] = 1;
                        v[k] = i;
                        BackSelectDame(v, selected, k + 1, n);
                        selected[i] = 0;
                    }
                }
            }
        }
        // aranjamente
        static void Aranjamente(int n, int k)
        {
            int[] v = new int[n];
            int[] sel = new int[n];
            BackA(v, sel, 0, n, k);
        }
        static void BackA(int[] v, int[] selected, int k, int n, int p)
        {
            // permutari
            if (k >= p)
            {
                for (int i = 0; i < p; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (selected[i] == 0)
                    {
                        selected[i] = 1;
                        v[k] = i;
                        BackA(v, selected, k + 1, n, p);
                        selected[i] = 0;
                    }
                }
            }
        }
        static void Combinari(int n, int k)
        {
            int[] v = new int[n + 1];
            BackC(v, 1, n, k + 1);
        }
        static void BackC(int[] v, int k, int n, int p)
        {
            // not working
            // permutari
            if (k >= p)
            {
                for (int i = 1; i < p; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = v[k - 1] + 1; i < n; i++)
                {
                    v[k] = i;
                    BackC(v, k + 1, n, p);                
                }
            }
        }

        //patratul magic
        static void MagicSquare(int n)
        {
            // todo
        }
        
    }
}
