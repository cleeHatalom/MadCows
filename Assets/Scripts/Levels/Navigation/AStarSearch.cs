using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Levels.Navigation
{
    /* NOTE about types: in the main article, in the Python code I just
 * use numbers for costs, heuristics, and priorities. In the C++ code
 * I use a typedef for this, because you might want int or double or
 * another type. In this C# code I use double for costs, heuristics,
 * and priorities. You can use an int if you know your values are
 * always integers, and you can use a smaller size number if you know
 * the values are always small. */

    public class AStarSearch
    {
        public Dictionary<NavigationNode, NavigationNode> cameFrom
            = new Dictionary<NavigationNode, NavigationNode>();
        public Dictionary<NavigationNode, double> costSoFar
            = new Dictionary<NavigationNode, double>();

        // Note: a generic version of A* would abstract over NavigationNode and
        // also Heuristic
        static public double Heuristic(NavigationNode a, NavigationNode b)
        {
            return Math.Abs(a.position.x - b.position.x) + Math.Abs(a.position.y - b.position.y);
        }

        static public double Cost(NavigationNode a, NavigationNode b)
        {
            return 1 + (b.position.z - a.position.z);
        }

        public AStarSearch(NavigationNode[,] graph, Vector2Int startPoint, Vector2Int goalPoint)
        {
            NavigationNode start = graph[startPoint.x, startPoint.y];
            NavigationNode goal = graph[goalPoint.x, goalPoint.y];

            var frontier = new PriorityQueue<NavigationNode>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                NavigationNode current = frontier.Dequeue();
                NavigationNode next;

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var nextPoint in (current.Neighbors))
                {
                    next = graph[nextPoint.x, nextPoint.y];

                    double newCost = costSoFar[current]
                        + Cost(current, next);
                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
        }
    }
}
