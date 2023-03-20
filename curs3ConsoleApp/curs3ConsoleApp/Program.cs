using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curs3ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            P1();
        }
        //a - nr maxim 8 cifre, b - nr de 1 cifra, sa se returnele numarul aparitiilor lui b din a
        static int cif(int a, int b)
        {
            int count = 0;
            while(a > 0)
            {
                if(a % 10 == b)
                {
                    count++;
                }
                a /= 10;
            }
            return count;
        }
        // o functie care citeste un nr de 8 cifre exact, sa se determine prin functia cif daca numarul poate forma un palindrom, sa se afiseze cel mai mare palindrom
        static void P1()
        {
            int nr = int.Parse(Console.ReadLine());
            int palindrom = 0;
            for (int i = 9; i >= 0; i--)
            {
                int nrcif = cif(nr, i);
                if (nrcif % 2 == 0)
                {
                    for(int k = 0; k < nrcif / 2; k++)
                    {
                        palindrom *= 10;
                        palindrom += i;
                    }                                      
                }
                else if(nrcif != 0)
                {
                    palindrom = 0;
                    break;
                }
            }
            int aux = palindrom;
            while(aux > 0)
            {
                palindrom *= 10;
                palindrom += aux % 10;
                aux /= 10;
            }
            Console.WriteLine(palindrom);
        }
        // se considera un sir crescator. se afiseaza numarul si de cate ori apare
        static void P2()
        {
            StreamReader sr = new StreamReader("text.in");
            int x = int.Parse(sr.ReadLine()); int y = 0;
            int count = 1;
            while (!sr.EndOfStream)
            {
                y = int.Parse(sr.ReadLine());
                if (y == x)
                {
                    count++;
                }
                else
                {
                    Console.Write(x+" "+ count);
                    count = 1;
                }
                x = y;
            }
            Console.Write(x + " " + count);


        }
        // perechi asemenea
        static void P3()
        {
            int n = 1;
            int m = 1;

            int[] a = new int[n];
            int[] b = new int[m];

        }
    }
}
