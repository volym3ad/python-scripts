using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ParallelProcesses
{
    class Massive
    {
        int begin, masQuantity;
        public int maxValue;
        int[] outline;
    
        Random rand = new Random();

        public Massive(int a, int b, int[] massv)
        {
            begin = a;
            masQuantity = b;
            outline = massv;
        }

        public void Run()
        {
            for (int i = begin; i < masQuantity; i++)
            {
                outline[i] = rand.Next(10000); //Math.Cos(Math.Sin(Math.Log10(i))); 
            }

            maxValue = outline[begin];
            for (int i = begin; i < masQuantity; i++)
            {
               if (outline[i] > maxValue)
                    maxValue = outline[i];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int simpleAmount = 2000;
            int[] mas = new int[simpleAmount];
            Massive max = new Massive(0, 1000, mas);
            Massive max2 = new Massive(1000, 1999, mas);
            Thread thr = new Thread(new ThreadStart(max.Run));
            Thread thr2 = new Thread(new ThreadStart(max2.Run));

            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();

            thr.Start();
            thr2.Start();

            thr.Join();
            thr2.Join();

            int mm1 = max.maxValue;
            int mm2 = max2.maxValue;
            int MAX1;
            if (mm1 > mm2)
                MAX1 = mm1;
            else
                MAX1 = mm2;

            stopwatch1.Stop();
            Console.WriteLine("\n------thread------ max-value:" + MAX1);
            Console.WriteLine("------thread------ time: " + stopwatch1.Elapsed + "\n\n");

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            Random rnd = new Random();
            int[] seq2 = new int[simpleAmount];
            for (int i = 0; i < simpleAmount; i++)
            {
                seq2[i] = rnd.Next(10000); //Math.Cos(Math.Sin(Math.Log10(i))); 
            }

            int MAX2 = seq2.Max<int>();

            stopwatch2.Stop();

            Console.WriteLine("------sequence------ max-value: " + MAX2);
            Console.WriteLine("------sequence------ time: " + stopwatch2.Elapsed);

            Console.ReadKey();
        }
    }
}
