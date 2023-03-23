using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curs4ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] apa = new int[] { 2, 5, 7, 1, 3, 5, 4, 0, 2, 3, 2 };
            //VectorApa(apa);
            //ElementeFrontiera();
            ProblemaCompletare(5,5);
        }
        // se da un vector ce reprezina altitudini. Se cere cata apa poate retine vectorul
        // metode aleatoare
        static void VectorApa(int[] v)
        {
            int apa = 0;
            int n = v.Length;
            /*
            Random rnd = new Random();           // metoda aleatoare
            for(int k = 0; k < 10000; k++)
            {
                int p = rnd.Next(n);
                bool bs = false;
                bool bd = false;
                for (int i = p - 1; i >= 0; i--)
                {
                    if (v[i] > v[p])
                    {
                        bs = true;
                        break;
                    }
                }
                for (int i = p + 1; i < n; i++)
                {
                    if (v[p] < v[i])
                    {
                        bd = true;
                        break;
                    }
                }
                if (bs && bd)
                {
                    apa++;
                    v[p]++;
                }
            }
            Console.WriteLine(apa);
            for(int i = 0; i < n; i++)
            {
                Console.Write(v[i] + " ");
            }
            */

            int max = 0;
            for(int i = 0; i < n; i++) 
            {
                if (v[i] > max)
                {
                    max = v[i];
                }
            }
            int[,] mat = new int[max, n];
            for(int i = 0; i < max; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (v[j] >= i)
                    {
                        mat[i, j] = 1;
                    }
                    //Console.Write(mat[i, j] + " ");
                }
                //Console.WriteLine();
            }
            for(int i = 0; i < max; i++)
            {
                int right = n - 1;
                int left = 0;
                while(right > 0)
                {
                    if (mat[i, right] == 1)
                    {
                        break;
                    }
                    else
                    {
                        mat[i, right] = 2;
                    }
                    right--;
                }
                while(left < n)
                {
                    if (mat[i,left] == 1)
                    {
                        break;
                    }
                    else
                    {
                        mat[i, left] = 2;
                    }
                    left++;
                }
            }
            Console.WriteLine();
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] == 0)
                    {
                        apa++;
                    }
                    Console.Write(mat[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(apa);
        }
        static void SteagulBritaniei(int[,] mat)
        {
            int n = mat.GetLength(0);
            for(int i = 0; i < n; i++) // O(n)
            {
                mat[i, i] = 1;
                mat[i, n / 2] = 1;
                mat[n / 2, i] = 1;
                mat[i, n - i - 1] = 1;
            }
        }
        static void ElementeFrontiera()
        {
            TextReader load = new StreamReader(@"..\..\Text.txt");
            List<string> T = new List<string>();
            string buffer;
            while((buffer = load.ReadLine()) != null) 
            {
                T.Add(buffer);
            }
            load.Close();
            int n = T.Count;
            int m = T[0].Split(' ').ToArray().Length;
            int[,] mat = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                string[] local = T[i].Split(' ');
                for (int j = 0; j < m; j++)
                {
                    mat[i, j] = int.Parse(local[j]);
                    //Console.Write(mat[i, j]);
                }
                //Console.WriteLine();
            }
            int k = 0; // TODO COMPLETEAZA ALGORITMUL PARAMETRIZAT
            for(int i = k; i < n - 1 - k; i++)
            {
                Console.Write(mat[k, i]);
            }
            for (int i = k; i < n - 1 - k; i++)
            {
                Console.Write(mat[i, n - 1 - k]);
            }
            for (int i = n - 1 - k; i >= 1 + k; i--)
            {
                Console.Write(mat[n - 1 - k, i]);
            }
            for (int i = n - 1 - k; i >= 1 + k; i++)
            {
                Console.Write(mat[i, k]);
            }
        }
        // se da prima linie dintr-o matrice 0,1,2,...,n-1
        // se cere completarea matricii a.i. fiecare element aflat pe pozitia i,j sa nu se mai repete pe linia i si coloana j
        // se cere elementul cu valoarea minima
        static void ProblemaCompletare(int n, int m)
        {
            int[,] mat = new int[m, n];
            for(int i = 0; i < n; i++)
            {
                mat[0, i] = i;
            }
            for(int i = 1; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    /*
                    bool[] T = new bool[n * m];
                    for (int k = 0; k < i; k++)
                    {
                        T[mat[k, j]] = true;
                    }
                    for (int k = 0; k < j; k++)
                    {
                        T[mat[i, k]] = true;
                    }
                    int idx = 0;
                    while (T[idx])
                    {
                        idx++;
                    }
                    mat[i, j] = idx;
                    */
                    mat[i, j] = i ^ j;
                }
            }


            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Console.Write(mat[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
