/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            Task<int[]> firstTask = Task.Run(() =>
            {
                Console.WriteLine("First Task – create an array of 10 random integer.");

                const int arrayLength = 10;

                int[] arrayWithRandomValues = new int[arrayLength];

                for (int i = 0; i < arrayLength; ++i)
                {
                    arrayWithRandomValues[i] = getRandomNumber();
                    Console.WriteLine($"Index:{i}, Value:{arrayWithRandomValues[i]}");
                }

                Console.WriteLine("");

                return arrayWithRandomValues;
            });

            Task<int[]> secondTask = firstTask.ContinueWith(x => 
            {
                int[] arrayWithRandomValues = firstTask.Result;
                int randomNumber = getRandomNumber();
                int arrayLength = arrayWithRandomValues.Length;

                Console.WriteLine($"Second Task – multiply array with {randomNumber}");

                for (int i = 0; i < arrayLength; ++i)
                {
                    arrayWithRandomValues[i] *= randomNumber;
                    Console.WriteLine($"Index:{i}, Value:{arrayWithRandomValues[i]}");
                }

                Console.WriteLine("");

                return arrayWithRandomValues;
            });

            Task<int[]> thirdTask = secondTask.ContinueWith(x =>
            {
                Console.WriteLine("Third Task – sort array by ascending.");

                int[] arrayWithRandomValues = secondTask.Result;
                int[] arrayAscendingSorted = arrayWithRandomValues.OrderBy(x => x).ToArray();

                printArray(arrayAscendingSorted);
                Console.WriteLine("");

                return arrayAscendingSorted;
            });

            Task<double> fourthTask = thirdTask.ContinueWith(x =>
            {
                Console.WriteLine("Fourth Task – calculate the average value.");

                int[] arrayDescendingSorted = thirdTask.Result;
                double averageOfArray = arrayDescendingSorted.Average();

                return averageOfArray;
            });

            double averageOfArray = fourthTask.Result;

            Console.WriteLine($"Average value of array is {averageOfArray}.");
            Console.WriteLine("");
            Console.WriteLine("Press <ENTER> to complete.");
            Console.ReadLine();
        }

        private static int getRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        private static void printArray(int[] arrayToPrint)
        {
            if (arrayToPrint is not null)
            {
                int arrayLength = arrayToPrint.Length;

                for (int i = 0; i < arrayLength; ++i)
                {
                    Console.WriteLine($"Index:{i}, Value:{arrayToPrint[i]}");
                }
            }
            else
            {
                Console.WriteLine("Cannot print empty array.");
            }
        }
    }
}
