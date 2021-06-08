using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Levels.Navigation
{
    public struct NavigationNode
    {
        public readonly Vector3 position;
        private Vector3Int intPosition;
        public readonly bool canTraverse;

        private List<Vector2Int> neighbors;
        public List<Vector2Int> Neighbors 
        { 
            get
            {
                return neighbors;
            }
        }

        public NavigationNode(Vector3 pos, bool traverse)
        {
            position = pos;
            intPosition = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
            canTraverse = traverse;

            neighbors = new List<Vector2Int>();
        }

        public bool Equals(NavigationNode node)
        {
            bool matchingNeighbors = node.Neighbors.Count == Neighbors.Count;

            if(matchingNeighbors)
            {
                for(int i = 0; i< Neighbors.Count; ++i)
                {
                    if(!node.Neighbors[i].Equals(neighbors[i]))
                    {
                        matchingNeighbors = false;
                        break;
                    }
                }
            }

            return matchingNeighbors && (node.canTraverse && this.canTraverse)
                && intPosition.Equals(node.intPosition);
        }

        public void AddNeighbor(Vector2Int neighbor)
        {
            neighbors.Add(neighbor);
        }
    }
}
