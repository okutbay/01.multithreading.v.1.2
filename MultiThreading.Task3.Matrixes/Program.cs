﻿/*
 * 3. Write a program, which multiplies two matrices and uses class Parallel.
 * a. Implement logic of MatricesMultiplierParallel.cs
 *    Make sure that all the tests within MultiThreading.Task3.MatrixMultiplier.Tests.csproj run successfully.
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
 */

using System;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
            Console.WriteLine();

            byte matrixSize = 7; // todo: use any number you like or enter from console
            matrixSize = GetUserMatrixSize(matrixSize);

            CreateAndProcessMatrices(matrixSize);
            Console.ReadLine();
        }

        private static byte GetUserMatrixSize(byte defaultMatrixSize)
        {
            Console.WriteLine($"Please enter your matrix size. (The value must be between 1-255. Otherwise default value will be used. Default Value is {defaultMatrixSize})");
            var matrixSizeString = Console.ReadLine();

            byte userMatrixSize = 0;
            byte.TryParse(matrixSizeString, out userMatrixSize);

            if (userMatrixSize == 0)
            {
                 userMatrixSize = defaultMatrixSize;
            }

            return userMatrixSize;
        }

        private static void CreateAndProcessMatrices(byte sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);
            var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);

            IMatrix resultMatrix = new MatricesMultiplier().Multiply(firstMatrix, secondMatrix);

            IMatrix resultMatrix2 = new MatricesMultiplierParallel().Multiply(firstMatrix, secondMatrix);

            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();
            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();
            Console.WriteLine("resultMatrix:");
            resultMatrix.Print();

            Console.WriteLine("resultMatrix2:");
            resultMatrix2.Print();
        }
    }
}
