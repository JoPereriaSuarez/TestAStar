using System;
using System.Text;

namespace AStar
{
    /// <summary>
    /// Representation of laberythm
    /// </summary>
    public class Matrix
    {
        public readonly int rows, columns;

        /// <summary>
        /// [Rows][Column]
        /// </summary>
        public int[][] Values { get; private set; }

        /// <summary>
        /// Row Column
        /// </summary>
        public int[] InitialPoint { get; private set; } = new int[2];

        /// <summary>
        /// Row Column
        /// </summary>
        public int[] DestinationPoint { get; private set; } = new int[2];

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            Values = new int[rows][];
        }

        public void AddColumnValues()
        {           
            string[] userValues;
            int tempValue;
            for (int i = 0; i < rows; i++)
            {
                int[] tempRowValues = new int[columns];
                bool isCorrect = false;
                do
                {
                    Console.WriteLine($"Add ({columns}) 0 or 1 values for row #{i+1}, separeted by a colon");

                    userValues = Console.ReadLine().Split(',');
                    if(userValues.Length < columns)
                    {
                        Console.WriteLine($"ERROR: {userValues.Length} is less than {columns}");
                        continue;
                    }

                    for (int j = 0; j < columns; j++)
                    {
                        if(!Int32.TryParse(userValues[j], out tempValue))
                        {
                            Console.WriteLine($"Error reading row #{i}");
                            break;
                        }

                        tempRowValues[j] = tempValue;
                    }
                    //for (int k = 0; k < temp_columns_values.Length; k++)
                    //{
                    //    Console.WriteLine(temp_columns_values[k]);
                    //}
                    isCorrect = true;
                }
                while (!isCorrect);
                Values[i] = tempRowValues;                
            }
        }

        public void SetDestinationPoint()
        {
            string[] temp_values = new string[2];
            do
            {
                Console.WriteLine("Set final point as an index of a Matrix");
                temp_values = Console.ReadLine().Split(',');
            }
            while 
            (!(Int32.TryParse(temp_values[0], out DestinationPoint[0])
            && Int32.TryParse(temp_values[1], out DestinationPoint[1])) );
        }

        public void SetInitialPoint()
        {
            string[] temp_values;
            do
            {
                Console.WriteLine("Set initial point as an index of a Matrix");
                temp_values = Console.ReadLine().Split(',');
            }
            while
            (!(Int32.TryParse(temp_values[0], out InitialPoint[0])
            && Int32.TryParse(temp_values[1], out InitialPoint[1])) );
        }

        public void PrintMatrix()
        {
            Console.WriteLine("The laberynth");
            StringBuilder puzzle = new StringBuilder();
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    puzzle.Append($"|{Values[i][j]}|");
                }
                puzzle.Append("\n");
            }

            Console.WriteLine(puzzle);
        }


    }

}
