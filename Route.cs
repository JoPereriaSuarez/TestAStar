using System;
using System.Text;
using System.Collections.Generic;

namespace AStar
{
    public class Route : IComparable<Route>
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();
        public List<Route> ChildrenRoutes { get; private set; } = new List<Route>();
        readonly Route parentRoute;
        readonly int initialParentIndex;

        public Node FinalNode
        {
            get { return Nodes[Nodes.Count - 1]; }
        }

        public Route(Route parentRoute, int initialParentIndex)
        {
            this.parentRoute = parentRoute;
            this.initialParentIndex = initialParentIndex;
        }
        public Route () : this (null, 0) { }

        public int SumF()
        {
            int value = 0;
            for (int i = Nodes.Count-1; i >= 0; i--)
            {
                value += Nodes[i].FValue;
            }
            if(parentRoute != null)
            {
                value += parentRoute.SumF(initialParentIndex - 1);
            }

            return value;
        }
        public int SumF(int fromIndex)
        {
            int value = 0;
            if(fromIndex >= 0)
            {
                for (int i = fromIndex; i >= 0; i--)
                {
                    value += Nodes[i].FValue;
                }
            }
            if (parentRoute != null)
            {
                value += parentRoute.SumF(initialParentIndex - 1);
            }

            return value;
        }

        public void Print()
        {
            StringBuilder routeText = new StringBuilder($"  TOTAL F = {SumF()} \n");
            for(int i = 0; i < Nodes.Count; i++)
            {
                routeText.Append($"{Nodes[i].index[0]}, {Nodes[i].index[1]} F = {Nodes[i].FValue}");
            }

            if(parentRoute != null)
            {
                for (int i = 0 ; i <initialParentIndex; i++)
                {
                    routeText.Append($" {parentRoute.Nodes[i].index[0]}, {parentRoute.Nodes[i].index[1]} F = {parentRoute.Nodes[i].FValue}\n");
                }
            }

            routeText.Append("\n");
            Console.WriteLine(routeText);
        }

        public int CompareTo(Route other)
        {
            int otherF = other.SumF();
            int thisF = SumF();

            return (thisF < otherF) ? -1 : (thisF > otherF)? 1 : 0;
        }


    }
}
