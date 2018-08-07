using System;
using System.Collections.Generic;

namespace AStar
{
    public class Node : IComparable<Node>
    {
        private readonly float h;
        private readonly int g;

        public readonly bool isEmpty;
        /// <summary>
        /// Index of the node on the main matrix. Row, Column
        /// </summary>
        public readonly int[] index = new int[2];

        public int FValue { get { return (int) (g + h); } }

        public Node(bool isEmpty,float h, int row, int column, int g = 1)
        {
            this.isEmpty = isEmpty;
            this.g = g;
            this.h = h;
            index[0] = row;
            index[1] = column;
        }

        public int CompareTo(Node other)
        {
            return (other.FValue > FValue) ? -1 : (other.FValue < FValue) ? 1 : 0;
        }

    }
}
