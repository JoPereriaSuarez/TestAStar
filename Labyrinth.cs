using System;
using System.Collections.Generic;

namespace AStar
{

    public class Labyrinth
    {
        List<Node> openList     = new List<Node>();
        List<Node> closedList   = new List<Node>();
        List<Route> routes      = new List<Route>();

        readonly int matrixRows, matrixColumns;
        readonly int[] initialIndex, finalIndex;

        public Labyrinth(Matrix matrix)
        {
            matrixRows = matrix.rows;
            matrixColumns = matrix.columns;
            initialIndex = matrix.InitialPoint;
            finalIndex = matrix.DestinationPoint;

            int rowDistance, columnDistance, heuristic;

            for (int i = 0; i < matrix.columns; i++)
            {
                rowDistance = Math.Abs(i - matrix.DestinationPoint[0]);
                for (int j = 0; j < matrix.rows; j++)
                {
                    if (matrix.Values[j][i] == 1) { continue; }

                    columnDistance = Math.Abs(j - matrix.DestinationPoint[1]);
                    heuristic = rowDistance + columnDistance;
                    openList.Add(new Node(false, heuristic, i, j));            
                }
            }
        }

        /// <summary>
        /// Returns the adyacent nodes to the paramenter one
        /// the arrays is sorted from the less F value
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private Node[] GetAdyacentNodes(Node current)
        {
            List<Node> foundNodes = new List<Node>();
            Node tempNode;
            int tempIndex;
            int row = current.index[0];
            int column = current.index[1];

            // Check left Node
            tempIndex = row - 1;
            if(tempIndex >= 0)
            {
                tempNode = openList.Find((Node node) => node.index[0] == tempIndex && node.index[1] == column); 
                if(tempNode != null)
                {
                    foundNodes.Add(tempNode);
                }
            }

            // Check right Node
            tempIndex = row + 1;
            if(tempIndex < matrixColumns)
            {
                tempNode = openList.Find((Node node) => node.index[0] == tempIndex && node.index[1] == column);
                if (tempNode != null)
                {
                    foundNodes.Add(tempNode);
                }
            }

            // Check upper Node
            tempIndex = column - 1;
            if(tempIndex >= 0)
            {
                tempNode = openList.Find((Node node) => node.index[0] == row && node.index[1] == tempIndex);
                if (tempNode != null)
                {
                    foundNodes.Add(tempNode);
                }
            }

            // Check below Node
            tempIndex = column + 1;
            if (tempIndex < matrixRows)
            {
                tempNode = openList.Find((Node node) => node.index[0] == row && node.index[1] == tempIndex);
                if (tempNode != null)
                {
                    foundNodes.Add(tempNode);
                }
            }

            foundNodes.Sort();
            return foundNodes.ToArray();
        }

        public void Solve()
        {
            Node initialNode = openList.Find((Node node) => node.index[0] == initialIndex[0] && node.index[1] == initialIndex[1]);
            Route initialRoute = new Route();
            initialRoute.Nodes.Add(initialNode);
            openList.Remove(initialNode);
            closedList.Add(initialNode);

            EvaluateRoutes(initialRoute);

            routes.Sort();

        }

        public void PrintSolution()
        {
            Console.WriteLine();
            if(routes.Count <= 0)
            {
                Console.WriteLine($"A* has not found any route that get to the solution point {finalIndex[0]}, {finalIndex[1]}");
            }
            else
            {
                for (int i = 0; i < routes.Count; i++)
                {
                    Console.WriteLine($"ROUTE FOUND # {i}");
                    routes[i].Print();
                }
            }

            Console.WriteLine("FINISH A*");
        }

        public void EvaluateRoutes(Route current)
        {
            while (current.FinalNode.index[0] != finalIndex[0] || current.FinalNode.index[1] != finalIndex[1])
            {
                Node[] adyacentNodes = GetAdyacentNodes(current.FinalNode);
                if(adyacentNodes == null || adyacentNodes.Length == 0) { break; }
                
                for (int i = 0; i < adyacentNodes.Length; i++)
                {
                    if(i == 0)
                    {
                        current.Nodes.Add(adyacentNodes[0]);
                        if(adyacentNodes[i].index[0] != finalIndex[0] || adyacentNodes[i].index[1] != finalIndex[1])
                        {
                            openList.Remove(adyacentNodes[i]);
                        }
                        continue;
                    }

                    if (adyacentNodes[i].index[0] != finalIndex[0] || adyacentNodes[i].index[1] != finalIndex[1])
                    {
                        openList.Remove(adyacentNodes[i]);
                    }
                    Route childRoute = new Route(current, current.Nodes.Count - 1);
                    childRoute.Nodes.Add(adyacentNodes[i]);
                    current.ChildrenRoutes.Add(childRoute);
                }
            }

            bool hasFoundSolution = (current.FinalNode.index[0] == finalIndex[0] && current.FinalNode.index[1] == finalIndex[1]);
            if(hasFoundSolution) { routes.Add(current); }

            for (int i = 0; i < current.ChildrenRoutes.Count; i++)
            {
                EvaluateRoutes(current.ChildrenRoutes[i]);
            }
        }
    }

}
