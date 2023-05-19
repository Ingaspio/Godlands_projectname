using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Pathfinding 
{
    //https://www.youtube.com/watch?v=P7sFfFLH4iM&ab_channel=SpontaneousSimulations pathfinding ultimate(independent) solution from SpontaneousSimulations
    public class UltimatePathfinding<node>
    {
        int calculatorPatience = 9999; // use to break the calculations of pathfinder, if iterations are too many

        public Func<node, node, float> GetHeuristicDistance;
        public Func<node, Dictionary<node, float>> GetNeighborsAndStepCosts;

        struct nodeData 
        {
            public float gCost;
            public float hCost;
            public float fCost { get=> gCost + hCost; }
        }

        public bool GeneratePath(node startNode, node endNode, out List<node> path) 
        {
            int patience = calculatorPatience;
            HashSet<node> CLOSED = new HashSet<node>();
            Dictionary<node, nodeData> OPEN = new Dictionary<node, nodeData>() { {startNode, new nodeData {gCost = 0f, hCost = GetHeuristicDistance(startNode, endNode) } } };
            Dictionary<node, node> DIRECTIONS = new Dictionary<node, node>();

            while (patience > 0) 
            {
                patience--;
                if (OPEN.Count == 0) break;
                node currentNode = OPEN.Aggregate((l, r) => l.Value.fCost < r.Value.fCost ? l : r).Key;
                nodeData currentNodeData = OPEN[currentNode];

                OPEN.Remove(currentNode);
                CLOSED.Add(currentNode);

                if (currentNode.Equals(endNode)) 
                { 
                    List<node> finalPath = new List<node>();
                    node tracebackStep = currentNode;
                    while (!tracebackStep.Equals(startNode)) 
                    {
                        finalPath.Add(tracebackStep);
                        tracebackStep = DIRECTIONS[tracebackStep];
                    }
                    finalPath.Reverse();
                    path = finalPath;
                    return true;
                }

                foreach(KeyValuePair<node, float> neighbourDistancePair in GetNeighborsAndStepCosts(currentNode)) 
                {
                    node neighbour = neighbourDistancePair.Key;
                    float currentToNeighbourDistance = neighbourDistancePair.Value;
                    if (CLOSED.Contains(neighbour)) continue;
                    float startToNeighbourDistance = currentToNeighbourDistance + currentNodeData.gCost;

                    if(!OPEN.ContainsKey(neighbour) || OPEN[neighbour].gCost > startToNeighbourDistance) 
                    {
                        DIRECTIONS[neighbour] = currentNode;
                        float targetToNeighbourDistance = GetHeuristicDistance(neighbour, endNode);
                        if (!OPEN.ContainsKey(neighbour)) 
                        {
                            OPEN.Add(neighbour, new nodeData { gCost = startToNeighbourDistance, hCost = targetToNeighbourDistance });
                        }
                        else
                        {
                            OPEN[neighbour] = new nodeData { gCost = startToNeighbourDistance, hCost = targetToNeighbourDistance };
                        }
                    }
                }
            }
            path = new List<node>();
            return false;
        }
    }
}

