/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static readonly object someLock = new object();
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            CreateRecursiveThreads();

            Console.ReadLine();
        }

        private static void CreateRecursiveThreads(int? state = 10)
        {
            if (state > 0)
            {
                Thread thread = new Thread(() => DisplayState(state));
                thread.Start();
                state = state - 1;
                CreateRecursiveThreads(state);
            }
        }

        private static void DisplayState(int? state)
        {
            semaphoreSlim.Wait();
            Console.WriteLine($"state: {state}");
            semaphoreSlim.Release();
        }
    }
}
