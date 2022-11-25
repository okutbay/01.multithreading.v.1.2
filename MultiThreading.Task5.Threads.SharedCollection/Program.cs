/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation 
 * and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static Dictionary<int, string> items = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection");
            Console.WriteLine("and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation");
            Console.WriteLine("and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            var task1 = Task.Factory.StartNew(() => addItem());
            var task2 = Task.Factory.StartNew(() => printItem());
            Task.WaitAll(task1, task2);

            Console.ReadLine();
        }

        private static void addItem()
        {
            lock (items)
            {
                for (int i = 0; i < 10; i++)
                {
                    items.Add(items.Count, $"Value {items.Count}");
                }
            }
        }

        private static void printItem()
        {
            lock (items)
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
        }
    }
}
