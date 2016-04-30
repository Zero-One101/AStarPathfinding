namespace AStarPathfinding
{
    /// <summary>
    /// A class to hold the integer X and Y co-ordinates of a point in space
    /// </summary>
    class Point
    {
        /// <summary>
        /// The position on the X axis of the point
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The position on the Y axis of the point
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Creates a new Point at the specified co-ordinates
        /// </summary>
        /// <param name="x">The X co-ordinate</param>
        /// <param name="y">The Y co-ordinate</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
