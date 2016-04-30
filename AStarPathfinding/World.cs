using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathfinding
{
    /// <summary>
    /// Holds the world grid and the pathfinding
    /// </summary>
    class World
    {
        /// <summary>
        /// The 2D grid of nodes
        /// </summary>
        private Node[,] nodes;

        /// <summary>
        /// The starting position of the search
        /// </summary>
        private Node startNode;

        /// <summary>
        /// The target for the search to reach
        /// </summary>
        private Node endNode;

        /// <summary>
        /// The width of the grid
        /// </summary>
        private int width;

        /// <summary>
        /// The height of the grid
        /// </summary>
        private int height;

        /// <summary>
        /// Creates a new world object and executes the search on the given map
        /// </summary>
        /// <param name="searchParams"></param>
        public World(SearchParams searchParams)
        {
            CreateWorld(searchParams);
            startNode = nodes[searchParams.StartPosition.X, searchParams.StartPosition.Y];
            endNode = nodes[searchParams.EndPosition.X, searchParams.EndPosition.Y];

            if (FindPath())
            {
                DisplayPath();
            }
        }

        /// <summary>
        /// Draws the map and the path between the start and end points to the console
        /// </summary>
        private void DisplayPath()
        {
            for (var y = 0; y < height; y++)
            {
                var displayLine = "";

                for (var x = 0; x < width; x++)
                {
                    if (nodes[x, y].IsPathNode)
                    {
                        displayLine += '*';
                    }
                    else if (nodes[x, y].Position == startNode.Position)
                    {
                        displayLine += 'S';
                    }
                    else if (nodes[x, y].Position == endNode.Position)
                    {
                        displayLine += 'T';
                    }
                    else if (nodes[x, y].IsWalkable)
                    {
                        displayLine += '.';
                    }
                    else
                    {
                        displayLine += 'X';
                    }
                }

                Console.WriteLine(displayLine);
            }
        }

        /// <summary>
        /// Reads the world from a .map file and populates the grid with Node objects
        /// </summary>
        /// <param name="searchParams"></param>
        private void CreateWorld(SearchParams searchParams)
        {
            var rows = File.ReadAllLines(searchParams.WorldMapFile);
            width = rows[0].Length;
            height = rows.Length;
            nodes = new Node[width, height];

            for (var y = 0; y < height; y++)
            {
                var cells = rows[y].ToCharArray();

                for (var x = 0; x < width; x++)
                {
                    if (cells[x] == '.')
                    {
                        var newNode = new Node(new Point(x, y), true, searchParams.EndPosition);
                        nodes[x, y] = newNode;
                    }
                    else
                    {
                        var newNode = new Node(new Point(x, y), false, searchParams.EndPosition);
                        nodes[x, y] = newNode;
                    }
                }
            }
        }

        /// <summary>
        /// Tries to find the path between the start and end points. If successful, traverses the nodes backwards and sets the IsPathNode variable
        /// </summary>
        /// <returns>True if the search was successful, else false</returns>
        private bool FindPath()
        {
            if (Search(startNode))
            {
                Console.WriteLine("We have reached the target.");

                var currentNode = endNode;
                while (currentNode.Parent != null)
                {
                    currentNode.IsPathNode = true;
                    currentNode = currentNode.Parent;
                }

                return true;
            }

            Console.WriteLine("A path to the target could not be found");
            return false;
        }

        /// <summary>
        /// Recursively iterates through each untested or open node until a path is found, or all nodes are closed
        /// </summary>
        /// <param name="currentNode">The node to search from</param>
        /// <returns>True if a path was found, else false</returns>
        private bool Search(Node currentNode)
        {
            currentNode.State = Node.NodeState.EClosed;
            var adjacentNodes = GetWalkableAdjacentNodes(currentNode);

            adjacentNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));

            foreach (var node in adjacentNodes)
            {
                if (node.Position == endNode.Position)
                {
                    return true;
                }

                if (Search(node))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the 8 points surrounding a position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Returns an array of 8 points that surround the position</returns>
        private Point[] GetAdjacentPoints(Point position)
        {
            return new[]
            {
                new Point(position.X - 1, position.Y - 1),
                new Point(position.X, position.Y - 1),
                new Point(position.X + 1, position.Y - 1),
                new Point(position.X - 1, position.Y),
                new Point(position.X + 1, position.Y),
                new Point(position.X - 1, position.Y + 1),
                new Point(position.X, position.Y + 1),
                new Point(position.X + 1, position.Y + 1)     
            };
        }

        /// <summary>
        /// Returns all valid, walkable nodes surrounding the current node
        /// </summary>
        /// <param name="currentNode">The current node</param>
        /// <returns>Returns a List of walkable nodes</returns>
        private List<Node> GetWalkableAdjacentNodes(Node currentNode)
        {
            var adjacentPoints = GetAdjacentPoints(currentNode.Position);
            var adjacentNodes = new List<Node>();

            foreach (var point in adjacentPoints)
            {
                if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height)
                {
                    continue;
                }

                var node = nodes[point.X, point.Y];

                if (node.State == Node.NodeState.EClosed || !node.IsWalkable)
                {
                    continue;
                }

                if (node.State == Node.NodeState.EOpen)
                {
                    var movementCost = node.GetMovementCost(node.Parent.Position);
                    var g = movementCost + currentNode.G;
                    if (g < node.G)
                    {
                        node.Parent = currentNode;
                        adjacentNodes.Add(node);
                        continue;
                    }
                }

                node.Parent = currentNode;
                node.State = Node.NodeState.EOpen;
                adjacentNodes.Add(node);
            }

            return adjacentNodes;
        } 
    }
}
