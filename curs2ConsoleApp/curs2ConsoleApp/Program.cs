using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace curs2ConsoleApp
{
    internal class Program
    {
        //https://invata.info/2017/09/25/rezolvare-bac-informatica-2009-varianta-69-subiectul-al-iii-lea-problema-4/
        static void Main(string[] args)
        {
            //P1();
            //P1_1();
            //P2();
            //int a = 25;
            //int b = 144;
            //int[] x = new int[] { 0, 2, 4, -5, 2, 5, 1, 2, 10, 11, 15, -2, 1 };
            //Console.WriteLine(SubprogramVar31(x, x.Length, 7));
            //Var31Ex4();
            //SubprogramPVar44(a, b);
            //Var69Ex4();
            int[] x = new int[] { 0, 2, 4, -5, 2, 5, 1, 2, 10, 11, 15, -2, 1 };
            InsertionSort(x, x.Length);
            Array.ForEach<int>(x, v => Console.Write(v + " "));
        }
        static void Var69Ex4()
        {
            StreamReader sr = new StreamReader(@"..\..\data.in");
            StringBuilder pare = new StringBuilder();
            StringBuilder impare = new StringBuilder();

            int n = int.Parse(sr.ReadLine());
            string[] tokens = sr.ReadLine().Split(' ');
            for(int i = 0; i < n; i++)
            {
                if (int.Parse(tokens[i]) % 2 == 0)
                {
                    pare.Append(tokens[i] + " ");
                }
                if (int.Parse(tokens[n - i - 1]) % 2 == 1)
                {
                    impare.Append(tokens[n - i - 1] + " ");
                }
            }
            Console.WriteLine(pare.ToString() + impare.ToString());

            sr.Close();
        }
        static void Var31Ex4()
        {
            StreamReader sr = new StreamReader(@"..\..\data.in");
            string line = sr.ReadLine();
            int n = int.Parse(line);
            int minsegment = int.MinValue;
            int maxsegment = int.MaxValue;
            for(int i = 0; i < n; i++)
            {
                line = sr.ReadLine();
                int x = int.Parse(line.Split(' ')[0]);
                int y = int.Parse(line.Split(' ')[1]);
                if(x > minsegment)
                {
                    minsegment = x;
                }
                if(y < maxsegment)
                {
                    maxsegment = y;
                }
            }
            if(minsegment > maxsegment)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(minsegment + " " + maxsegment);
            }
            sr.Close();
        }
        
        static void SubprogramPVar44(int a, int b)
        {
            int min = a, max = b;
            if (a > b)
            {
                min = b;
                max = a;
            }
            //for(int i = min + 1; i < max; i++)
            //{
            //    if(isPrim(Math.Sqrt(i)))
            //    {
            //        Console.Write(i + " ");
            //    }
            //}
            for (int i = (int)Math.Sqrt(min) + 1; i < (int)Math.Sqrt(max); i++)
            {
                if (isPrim(i))
                {
                    Console.Write(i * i + " ");
                }
            }

        }
        static int SubprogramVar31(int[] x, int n, int m)
        {
            int suma = 0;
            int[] fq = new int[n];
            for(int i = 0; i < m; i++)
            {
                int min = int.MaxValue;
                int minIndex = -1;
                for(int j = 0; j < n; j++)
                {
                    if (fq[j] == 1)
                    {
                        continue;
                    }
                    if(min > x[j])
                    {
                        min = x[j];
                        minIndex = j;
                    }
                }
                suma += min;
                fq[minIndex] = 1;
            }
            return suma;
        }

        static bool isPrim(double d)
        {
            //if(Math.Round(d,0) != d)
            //{
            //    return false;
            //}
            for(int i = 2; i <= d / 2; i++)
            {
                if(d % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        //Se da un numar intreg. Construiti numarul minim si maxim ce se poate obtine cu cifrele acestuia
        static void P1()
        {
            int[] fq = new int[10]; 
            int n = int.Parse(Console.ReadLine());
            while(n != 0)
            {
                fq[n % 10]++;
                n /= 10;
            }
            Console.WriteLine(Max(fq));
            Console.WriteLine(Min(fq));
        }
        static void P1_1()
        {
            int n = int.Parse(Console.ReadLine());
            int[] v = new int[16];
            int k = 0;
            while (n != 0)
            {
                v[k] = n % 10;
                k++;
                n /= 10;
            }
            BubbleSort(v, k);
            int max = 0;
            for(int i = k - 1; i >= 0; i--)
            {
                max = max * 10 + v[i];
            }

            int min = 0;
            if (v[0] == 0)
            {
                int idx = 1;
                while (v[idx] == 0)
                {
                    idx++;
                }
                (v[0], v[idx]) = (v[idx], v[0]);
            }
            for(int i = 0; i < k; i++)
            {
                min = min * 10 + v[i];
            }
            
        }
        static void P2()
        {
            // in fisierul data.in se gasesc nr intregi in intervalul -10^9  10^9
            // afisati cele mai mari 2 numere de 3 cifre care nu apar in fisier.
            string buffer;
            string[] local;
            int n;
            bool[] fq = new bool[1000];
            TextReader tr = new StreamReader(@"..\..\data.in");
            while((buffer = tr.ReadLine()) != null)
            {
                local = buffer.Split(' ');
                for(int i = 0; i < local.Length; i++)
                {
                    n = int.Parse(local[i]);
                    if(n > 99 && n < 1000)
                    {
                        fq[n] = true;
                    }
                }
            }
            int k = 0;
            for (int i = 999; i > 99; i--)
            {               
                if (!fq[i])
                {
                    Console.WriteLine(i + " ");
                    k++;
                    if (k == 2)
                    {
                        break;
                    }
                }
            }          
        }
        static void InsertionSort(int[]v, int k)
        {
            for(int j = 0; j < k; j++)
            {
                int i = j;
                while (i - 1 >= 0 && v[i] < v[i - 1])
                {
                    (v[i], v[i - 1]) = (v[i - 1], v[i]);
                    i--;
                }
            }
        }
        static void SelectionSort(int[]v, int k) // P3
        {
            // sortare2 : selection sort // P3
            int min;
            int poz;
            for (int j = 0; j < k - 1; j++)
            {
                min = v[j];
                poz = j;
                for (int i = j + 1; i < k; i++)
                {
                    if (v[i] < min)
                    {
                        min = v[i];
                        poz = i;
                    }

                }
                (v[j], v[poz]) = (v[poz], v[j]);
            }
        }
        static void BubbleSort(int[]v, int k) // P2
        {
            bool ok;
            int helper = 0;
            do
            {
                ok = true;
                for (int i = 0; i < k - 1 - helper; i++)
                {
                    if (v[i] > v[i + 1])
                    {
                        (v[i], v[i + 1]) = (v[i + 1], v[i]);
                        ok = false;
                    }
                }
                helper++;
            } while (!ok);
        }
        static int Min(int[] fq)
        {
            int toReturn = 0;
            if (fq[0] != 0)
            {
                int idx = 1;
                while (fq[idx] == 0)
                {
                    idx++;
                }
                toReturn = idx;
                fq[idx]--;
            }
            for(int i = 0; i < fq.Length; i++)
            {
                for(int j = 0; j < fq[i]; j++)
                {
                    toReturn *= 10;
                    toReturn += i;
                }
            }
            return toReturn;
        }
        static int Max(int[] fq)
        {
            int toReturn = 0;
            for (int i = 9; i >= 0; i--)
            {
                for(int j = 0; j < fq[i]; j++)
                {
                    toReturn *= 10;
                    toReturn += i;
                }
            }
            return toReturn;
        }
    }
}
