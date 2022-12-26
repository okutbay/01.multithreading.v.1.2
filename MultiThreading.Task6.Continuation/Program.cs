﻿/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail 
and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code

            var firstTask = Task.Factory.StartNew(() => First());

            var secondTask = firstTask.ContinueWith(x =>
            {
                if (x.Status == TaskStatus.Faulted)
                {
                    Console.WriteLine(x.Exception);
                }
                else
                {
                    Console.WriteLine(x.Result);
                }

                Console.WriteLine("started second task");
                Console.WriteLine(x.Result);
            });

            var onError = secondTask.ContinueWith(
                                  prev => Console.WriteLine(prev.Exception),
                                  TaskContinuationOptions.OnlyOnFaulted);

            var onSuccess = secondTask.ContinueWith(
                      prev => Console.WriteLine("secondTask success"),
                      TaskContinuationOptions.OnlyOnRanToCompletion | 
                      TaskContinuationOptions.ExecuteSynchronously |
                      TaskContinuationOptions.LongRunning);

            Console.ReadLine();
        }

        static string First() 
        {
            Console.WriteLine("started first task");
            Thread.Sleep(3000);
            Console.WriteLine("completed first task");
            return "some string";
        }

    }
}
