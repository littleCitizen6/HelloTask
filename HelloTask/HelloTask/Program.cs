using System;
using System.Threading;

namespace HelloTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Thread main = new Thread(() => TimerTread());
            main.Start();
            main.Join();
            Console.WriteLine((DateTime.Now-start).TotalMilliseconds);
        }
        public static void TimerTread()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Thread t = new Thread(() => {
                    int num = i;
                    Console.WriteLine(num);
                });
                t.Start();
            }
        }
    }
}
