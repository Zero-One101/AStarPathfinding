using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathfinding
{
    /// <summary>
    /// A class to contain the search parameters, e.g., start point, target point, map file
    /// </summary>
    class SearchParams
    {
        /// <summary>
        /// The point from which the search function will start from
        /// </summary>
        public Point StartPosition { get; private set; }

        /// <summary>
        /// The target point to search for
        /// </summary>
        public Point EndPosition { get; private set; }

        /// <summary>
        /// The name of the file holding the map of the grid
        /// </summary>
        public string WorldMapFile { get; private set; }

        /// <summary>
        /// Creates the search parameters
        /// </summary>
        /// <param name="startPosition">The positions to start the search from</param>
        /// <param name="endPosition">The position of the end point</param>
        /// <param name="worldMapFile">The filename of the map file</param>
        public SearchParams(Point startPosition, Point endPosition, string worldMapFile)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            WorldMapFile = worldMapFile;
        }
    }
}
