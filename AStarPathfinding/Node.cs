using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathfinding
{
    /// <summary>
    /// The points of the grid which the A* algorithm traverses
    /// </summary>
    class Node
    {
        /// <summary>
        /// The set of possible values of the checking state of the node
        /// </summary>
        public enum NodeState
        {
            EUntested, // Node hasn't been checked
            EOpen, // Node is being checked
            EClosed // Node has been checked
        };

        /// <summary>
        /// The checking state of the node
        /// </summary>
        public NodeState State { get; set; } = NodeState.EUntested;

        /// <summary>
        /// The position of the node
        /// </summary>
        public Point Position { get; private set; }
        private Node parent;

        /// <summary>
        /// The parent node of this node
        /// </summary>
        public Node Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                G = parent.G + GetMovementCost(parent.Position);
            }
        }

        /// <summary>
        /// Whether or not the node can be traversed
        /// </summary>
        public bool IsWalkable { get; private set; }

        /// <summary>
        /// Whether or not the node is on the path between the start and end points
        /// </summary>
        public bool IsPathNode { get; set; } = false;

        /// <summary>
        /// The distance travelled on the current path
        /// </summary>
        public float G { get; private set; }

        /// <summary>
        /// The Euclidean distance between this node and the end point
        /// </summary>
        public float H { get; private set; }

        /// <summary>
        /// The total estimated cost of this path
        /// </summary>
        public float F => G + H;

        /// <summary>
        /// Creates a new node
        /// </summary>
        /// <param name="position">The position of the node</param>
        /// <param name="isWalkable">Whether or not this node blocks the algorithm</param>
        /// <param name="endPoint">The end point for the algorithm</param>
        public Node(Point position, bool isWalkable, Point endPoint)
        {
            Position = position;
            IsWalkable = isWalkable;
            H = GetMovementCost(endPoint);
            G = 0;
        }

        /// <summary>
        /// Returns the Euclidean (straight-line) distance between this node and the target
        /// </summary>
        /// <param name="target">The position of the target</param>
        /// <returns>The Euclidean distance</returns>
        public float GetMovementCost(Point target)
        {
            var deltaX = target.X - Position.X;
            var deltaY = target.Y - Position.Y;
            return (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }
    }
}
