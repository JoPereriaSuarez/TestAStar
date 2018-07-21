using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Node
    {
        private readonly float h;
        private readonly int g;

        public readonly bool isEmpty;
        public List<Node> Children { get; set; }
        public Node Parent { get; private set; }
        public readonly int[] index = new int[2];

        /// <summary>
        /// A parent node can be added only once
        /// Add this node on the parent's childrend nodes
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(Node parent)
        {
            if(parent == this || parent == null) { return; }
            Parent = parent;
            Parent.Children.Add(this);
        }
        public bool isParentOf(Node node)
        {
            return (node != null &&
                node.Parent != null && 
                node.Parent == Parent);
        }

        public float FValue { get { return g + h; } }

        public Node(bool isEmpty,float h, int row, int column, int g = 1)
        {
            this.isEmpty = isEmpty;
            Children = new List<Node>();
            this.g = g;
            this.h = h;
            index[0] = row;
            index[1] = column;
        }
    }
}
