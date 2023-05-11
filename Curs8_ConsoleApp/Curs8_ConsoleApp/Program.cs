using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Curs8_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TT();
            //P1();
            //P2();
            //P3();
        }
        // se da o suma S
        // se da un set de valori ( bancnote )
        // se cere sa se scrie S ca si o combinatie de elemente V
        // se cere numarul minim de bancnote care se poate folosi
        async static void TT()
        {
            T1();
            T2();
        }
        async static void T1()
        {
            while (true)
            {
                Console.Write("1");
                await Task.Delay(100);
            }
        }
        async static void T2()
        {
            while (true)
            {
                Console.Write("2");
                await Task.Delay(1000);
            }
        }
        static void P1()
        {
            int[] v = new int[] { 1000, 500, 100, 50, 20, 10, 5, 1 };
            int S = int.Parse(Console.ReadLine());
            int[] r = new int[v.Length];
            
            for(int i = 0; i < v.Length; i++)
            {
                r[i] = S / v[i];
                S = S % v[i];
            }
            // deocamdata algoritmul asta nu functioneaza pentru orice caz in care V are elemente ciudate.
        }


        /// <summary>
        /// se dau N spectacole (prin timpul initial si timpul final ),
        /// ora la care incepe si ora la care se termina,
        /// avand la dispozitie o singura scena, se cere sa se programeze un numar maxim de spectacole ( sa nu existe mai mult de 1 spectacol pe sscena in acelasi timp )
        /// </summary>
        static void P2()
        {
            List<int[]> spectacole = new List<int[]>() 
            { 
                new int[] { 3, 5 }, 
                new int[] { 1, 4 }, 
                new int[] { 2, 5 }, 
                new int[] { 1, 6 },  
                new int[] { 0, 3 },
                new int[] { 4, 5 },
                new int[] { 6, 7 },
                new int[] { 5, 8 },
                new int[] { 4, 7 },
                new int[] { 2, 5 },
                new int[] { 0, 3 },
                new int[] { 0, 4 }
            };
            // sortam crescator dupa timpul de terminare
            for(int i = 0; i <  spectacole.Count; i++)
            {
                for(int j = 0; j < spectacole.Count; j++)
                {
                    if (spectacole[i][1] < spectacole[j][1])
                    {
                        (spectacole[i], spectacole[j]) = (spectacole[j], spectacole[i]);
                    }
                }
            }
            int count = 0;
            List<int> OK = new List<int>();
            int currenttime = spectacole[0][1];
            for(int i = 0; i < spectacole.Count; i++)
            {
                if (spectacole[i][0] >= currenttime)
                {
                    currenttime = spectacole[i][1];
                    OK.Add(i);
                }
            }
            foreach (int i in OK)
            {
                Console.WriteLine($"{spectacole[i][0]} {spectacole[i][1]}"); // speectacol.tostring()
            }
        }
        class Spectacol // uhh n-am chef sa rescriu codu de sus
        {
            public int[] time;
            public Spectacol(int s, int f)
            {
                time = new int[] { s, f };
            }
            public override string ToString()
            {
                return (time[0] + " " + time[1]);
            }
        }

        /// <summary>
        /// se da un numar in scrierea romana
        /// se cere valoarea acestuia
        /// </summary>
        static void P3()
        {
            string nr = "MMCMLXXIV"; // Console.ReadLine()
            Dictionary<char, int> Romans = new Dictionary<char, int>() { { 'M', 1000 }, { 'D', 500 }, { 'C', 100 },{ 'L', 50 }, { 'X', 10 }, { 'V', 5 }, { 'I', 1 }  };
            char[] nrc = nr.ToUpper().ToCharArray();
            int res = 0;
            for(int i = 0; i < nrc.Length - 1; i++) 
            {
                if (Romans[nrc[i]] >= Romans[nrc[i + 1]])
                {
                    res += Romans[nrc[i]];
                }
                else
                {
                    res -= Romans[nrc[i]];
                }
            }
            res += Romans[nrc.Last()];
            Console.WriteLine(res);
            // Algoritmul greedy
        }

        // Ana are 5 mere si 25 pere
        // trebuie transmis prin biti textul de sus
        // urmeaza anul viitor

        /// <summary>
        /// se da un numar arab, sa se transforme in scrierea romana
        /// </summary>
        static void P4()
        {
            int nr = 2917;
            int[] v = new int[] { 1000, 500, 100, 50, 10, 5, 1 };
            int[] r = new int[v.Length];
            for(int i = 0; i < v.Length; i++)
            {
                r[i] = nr / v[i];
                nr = nr % v[i];
            }
            // todo finish up
        }
        void F1(int x, char low, char mid, char high)
        {
            switch (x)
            {
                case 1: 
                    Console.Write(low);
                    break;
                case 2:
                    Console.Write(low + low);
                    break;
                case 3: 
                    Console.Write(low + low + low);
                    break;
                case 4:
                    Console.Write(low + mid);
                    break;
                case 5:
                    Console.Write(mid);
                    break;
                case 6:
                    Console.Write(mid + low);
                    break;
                case 7:
                    Console.Write(mid + low + low);
                    break;
                case 8:
                    Console.Write(mid + low + low + low);
                    break;
                case 9:
                    Console.Write(low + high);
                    break;
            }
        }

        // pt lab:
        // - suma
        // - problema spectacolelor
        // - Roman to Arab
        // - Arab to Roman
        // - Huffman tree (?)
    }
}
