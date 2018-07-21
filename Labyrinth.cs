using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Labyrinth
    {
        /// <summary>
        /// A represention of a matrix of nodes. Row, Column
        /// </summary>
        private readonly Node[][] nodeMatrix;

        /// <summary>
        /// Nodes that have been checked
        /// </summary>
        private List<Node> closeNodes = new List<Node>();

        /// <summary>
        /// Index of the initial node. Rown, Column
        /// </summary>
        private int[] initialPosition;

        /// <summary>
        /// Index of the destination node. Rown, Column
        /// </summary>
        private int[] destinationPosition;
        

        /// <summary>
        /// Initialize the a matrix of nodes
        /// </summary>
        /// <param name="matrix"></param>
        public Labyrinth(Matrix matrix)
        {
            float heuristic;
            nodeMatrix = new Node[matrix.rows][];
            for (int i = 0; i < matrix.rows; i++)
            {
                destinationPosition = matrix.DestinationPoint;
                initialPosition = matrix.InitialPoint;
                int rowHeuristic = (destinationPosition[0] - i) * (destinationPosition[0] - i);
                int columnHeuristic;
                Node[] tempNodeColumn = new Node[matrix.Values[i].Length];
                for (int j = 0; j < matrix.columns; j++)
                {
                    columnHeuristic = (destinationPosition[1] - j) * (destinationPosition[1] - j);
                    heuristic = (float)Math.Sqrt(rowHeuristic + columnHeuristic);
                    if(matrix.Values[i][j] == 1)
                    {
                        heuristic *= 2;
                    }
                    tempNodeColumn[j] = new Node((matrix.Values[i][j] == 0), heuristic, i, j);
                }
                nodeMatrix[i] = tempNodeColumn;
            }
        }

        public bool Solve()
        {
            closeNodes.Clear();
            closeNodes.TrimExcess();
            closeNodes.Add(nodeMatrix[initialPosition[0]][initialPosition[1]]);

            Node currentNode = closeNodes[0];
            Node initialNode = currentNode;
            Node destinationNode = nodeMatrix[destinationPosition[0]][destinationPosition[1]];

            while(currentNode != destinationNode)
            {
                // Find Better adyacent Node
                Node tempNode = FindFromOpenList(currentNode.index[0], currentNode.index[1]);
                if(tempNode == null)
                {
                    Console.WriteLine($"NO SOLUTION FOUND AT {currentNode.index[0]}, {currentNode.index[1]}");
                    break;
                }
                else
                {
                    closeNodes.Add(tempNode);
                    currentNode = tempNode;
                }
            }

            Console.WriteLine("SOLUTION FOUND");

            StringBuilder result = new StringBuilder();
            foreach (Node node in closeNodes)
            {
                result.Append($"{node.index[0]}, {node.index[1]} \n");
            }

            Console.WriteLine(result);
            return false;
        }

        /// <summary>
        /// Get the lowest f node that is not a wall and is not on closed list
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private Node FindFromOpenList(int row, int column)
        {
            Node adyacentNode = GetLowestNode(row, column);
            if(adyacentNode != null)
            {
                int onCloseList = closeNodes.FindIndex((Node node) => node == adyacentNode);
                return (adyacentNode.isEmpty && onCloseList == -1) ? adyacentNode : null;
            }

            return null;
        }

        /// <summary>
        /// Returns the adyacent Node with the lowest F value
        /// </summary>
        /// <returns></returns>
        private Node GetLowestNode(int row, int column)
        {
            float lowestF = nodeMatrix[row][column].FValue;
            int testIndex;
            Node tempNode = null;

            // Check left Node
            testIndex = row - 1;
            if(testIndex > 0 && GetLowestF(testIndex, column, ref lowestF))
            {
                tempNode = nodeMatrix[testIndex][column];
            }

            // Check right Node
            testIndex = row + 1;
            if (testIndex < nodeMatrix.Length && GetLowestF(testIndex, column, ref lowestF))
            {
                tempNode = nodeMatrix[testIndex][column];
            }

            // Check up Node
            testIndex = column - 1;
            if(testIndex > 0 && GetLowestF(row, testIndex, ref lowestF))
            {
                tempNode = nodeMatrix[row][testIndex];
            }

            // Check down Node
            testIndex = column + 1;
            if (testIndex < nodeMatrix[row].Length && GetLowestF(row, testIndex, ref lowestF))
            {
                tempNode = nodeMatrix[row][testIndex];
            }

            return tempNode;
        }

        public void PrintFValues()
        {
            Console.WriteLine("F VALUES ARE:");
            StringBuilder stringBuilder = new StringBuilder();
            for(int  i = 0; i < nodeMatrix.Length; i++, stringBuilder.Append("\n"))
            {
                for (int j = 0; j < nodeMatrix[i].Length; j++)
                {
                    stringBuilder.Append($"{i},{j} = {nodeMatrix[i][j].FValue}  ");
                }
            }

            Console.WriteLine(stringBuilder);
        }

        private bool GetLowestF(int row, int column, ref float value)
        {
            float nodeF;
            if (nodeMatrix[row][column] == null)
            {
                Console.WriteLine($"Node at {row}, {column} is null");
            }
            else if (nodeMatrix[row][column].FValue < value)
            {
                value = nodeMatrix[row][column].FValue;
                return true;
            }
            nodeF = nodeMatrix[row][column].FValue;
            return false;
        }
    }
}
