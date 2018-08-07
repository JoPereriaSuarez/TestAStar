using System;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int height, width;
            do
            {
                Console.WriteLine("Add laberythm Height");
            }
            while (!Int32.TryParse(Console.ReadLine(), out height) || height <= 0);
            Console.WriteLine($"Height is {height}");

            do
            {
                Console.WriteLine("Add laberythm Width");
            }
            while (!Int32.TryParse(Console.ReadLine(), out width) || width <= 0);
            Console.WriteLine($"Width is {width}");

            Console.WriteLine("Matrix Created Succeded");

            Matrix matrix = new Matrix(height, width);
            matrix.AddColumnValues();
            matrix.PrintMatrix();
            Console.WriteLine();

            matrix.SetInitialPoint();
            Console.WriteLine();

            matrix.SetDestinationPoint();

            Labyrinth labyrinth = new Labyrinth(matrix);
            labyrinth.Solve();
            labyrinth.PrintSolution();
            Console.WriteLine();

            Console.Read();
        }
    }
}
