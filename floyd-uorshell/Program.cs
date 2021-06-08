using System;
using System.Collections.Generic;
using System.IO;

namespace floyd_uorshell
{
    class Program
    {
        static void Main(string[] args)
        {
            (int, int) sizeGraph = (0, 0);
            List<(int, int, double)> edgesList = Read(ref sizeGraph);
            double[,] matrix = AdjMatrix(sizeGraph, edgesList);
            AlgoritmFlWo(ref matrix);
            ShowMatrix(matrix);
        }
        static List<(int, int, double)> Read(ref (int, int) sizeGraph)
        { 
            List<(int, int, double)> list = new List<(int, int, double)>();
            StreamReader read = new StreamReader("test.txt");
            string[] size = read.ReadLine()?.Split(' ');
            if (size != null)
            {
                sizeGraph = (Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                for (int i = 0; i < sizeGraph.Item2; ++i)
                {
                    size = read.ReadLine()?.Split(' ');
                    if (size != null)
                        list.Add((Convert.ToInt32(size[0]), Convert.ToInt32(size[1]), Convert.ToDouble(size[2])));
                }
            }
            return list;
        }
        static double[,] AdjMatrix((int, int) sizeMatrix, List<(int, int, double)> edgeList)
        {
            double[,] matrixA = new Double[sizeMatrix.Item1, sizeMatrix.Item1];
            for (int i = 0; i < sizeMatrix.Item1; i++)
            for (int j = 0; j < sizeMatrix.Item1; j++)
                    if (i == j)
                        matrixA[i, j] = 0;
                    else
                        matrixA[i, j] = double.PositiveInfinity;
            foreach (var item in edgeList)
                matrixA[item.Item1 - 1, item.Item2 - 1] = item.Item3;
            return matrixA;
        }
        static void AlgoritmFlWo(ref double[,] matrix)
        {
            for (int k = 0; k < Math.Pow(matrix.Length, 0.5); ++k)
            {
                for (int i = 0; i < Math.Pow(matrix.Length, 0.5); ++i)
                {
                    for (int j = 0; j < Math.Pow(matrix.Length, 0.5); ++j)
                    {
                        matrix[i, j] = MinDuo(matrix[i, j], matrix[i,k] + matrix[k,j]);
                    }
                }
                ShowMatrix(matrix);
            }
        }
        static void ShowMatrix(double[,] matrix)
        {
            for (int i = 0; i < Math.Pow(matrix.Length, 0.5); i++)
            {
                for (int j = 0; j < Math.Pow(matrix.Length, 0.5); j++)
                    if (double.IsPositiveInfinity(matrix[i,j]))
                        Console.Write(String.Format("{0,3}", "I"));
                    else
                        Console.Write(String.Format("{0,3}", matrix[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static double MinDuo(double a, double b)
        {
            if (a < b)
                return a;
            return b;
        }
    }
}