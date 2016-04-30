using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathfinding
{
    class Program
    {
        static void Main(string[] args)
        {
            var searchParams = new SearchParams(new Point(0, 0), new Point(4, 6), "world.map");
            var world = new World(searchParams);
        }
    }
}
