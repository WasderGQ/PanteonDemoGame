using System;

namespace YusufISIK.PathFinding
{
    [Serializable]
    public class Node : IHeapItem<Node>
    {
        public NodeType nodeType;
        public int x { get; } //X position of node
        public int y { get; } //Y position of node

        public Node parentNode;
        public int gCost, hCost; // A* calculation variables
        private int heapIndex;

        public Node(int x, int y, NodeType nodeType = NodeType.Empty)
        {
            this.nodeType = nodeType;
            this.x = x;
            this.y = y;
        }


        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    }

    [Serializable]
    public enum NodeType
    {
        Empty = 0,
        Blocked,
    }
}

