using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HelloTask
{
    class Program
    {
        static void Main1c(string[] args)
        {
            DateTime start = DateTime.Now;
            Thread main = new Thread(() => ThreadPoolUses());
            main.Start();
            main.Join();
            Console.WriteLine((DateTime.Now-start).TotalMilliseconds);
        }
        static void Main2a(string[] args)
        {
            ConcurrentStack<int> st = new ConcurrentStack<int>();
            for (int i = 0; i < 5000; i++)
            {
                st.Push(i);
            }
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 3; i++)
            {
                Task t = new Task(()=>PrintStackContentUntillEmpty(st));
                t.Start();
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray()) ;
        }
        static void Main(string[] args)
        {
            ConcurrentStack<int> st = new ConcurrentStack<int>();
            for (int i = 0; i < 5000; i++)
            {
                st.Push(i);
            }
            Parallel.For(0, 5000, index => PrintNum(index, st));
        }
        public static void PrintNum(int num, ConcurrentStack<int> st)
        {
            {
                int pop;
                if (st.TryPop(out pop))
                {
                    Console.WriteLine(pop);
                }
            }
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
        }
        public static async Task PrintStackContentUntillEmpty(ConcurrentStack<int> stack)
        {
            while (stack.Count > 0)
            {
                int pop;
                if (stack.TryPop(out pop))
                {
                    Console.WriteLine(pop);
                }
            }
        }
    }
}
