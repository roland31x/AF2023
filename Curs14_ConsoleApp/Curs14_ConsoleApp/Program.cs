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
            SaseZaruri();
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
    }
}
