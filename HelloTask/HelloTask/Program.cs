using System;
using System.Threading;

namespace HelloTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Thread main = new Thread(() => ThreadPoolUses());
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
        public static void sync1000000()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Console.WriteLine(i);
            }
        }
        public static void ThreadPoolUses()
        {

            for (int i = 0; i < 1000000; i++)
            {
                int num = i;
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Console.WriteLine(obj);
                },num);
            }
            Console.ReadLine();
        }
    }
}
