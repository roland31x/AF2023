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
        static void Main(string[] args)
        {
            //P1();
            //P1_1();
            P2();
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
                    if (v[i] < v[i + 1])
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
