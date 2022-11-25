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
        const int TaskAmount = 10;
        static int someState = 10;
        static int someState2 = 10;
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

            int workerThreads = 0;
            int completionPortThreads = 0;
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            ThreadPool.SetMaxThreads(workerThreads * 2, completionPortThreads * 2);

            for (int taskNumber = 1; taskNumber <= TaskAmount; taskNumber++)
            {
                new Thread(DisplayState).Start(taskNumber);

                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(DisplayState2), someState2);
            }

            Console.ReadLine();
        }

        private static void DisplayState(object state)
        {
            lock (someLock)
            {
                someState--;
                Console.WriteLine($"state1: {someState}");
            }
        }

        private static void DisplayState2(object state)
        {
            semaphoreSlim.Wait();

            lock (someLock)
            {
                int currentState = (int)state;
                currentState--;
                Console.WriteLine($"state2: {currentState}");
                someState2 = currentState;
            }

            semaphoreSlim.Release();
        }
    }
}
