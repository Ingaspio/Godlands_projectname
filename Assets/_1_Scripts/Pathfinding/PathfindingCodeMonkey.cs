using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


//AStar implementation CodeMonkey  https://www.youtube.com/watch?v=1bO1FdEThnU&t=392s&ab_channel=CodeMonkey

public class PathfindingCodeMonkey : MonoCache
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private Tilemap floor;
    private float tileAnchor = 0.5f;

    public void FindPath(int2 startPos, int2 endPos)
    {
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        int cellBoundsSummX = math.abs(floor.cellBounds.xMin - floor.cellBounds.xMax);
        int cellBoundsSummY = math.abs(floor.cellBounds.yMin - floor.cellBounds.yMax);
        NativeArray < PathNode > pathNodeArray = new NativeArray<PathNode>((cellBoundsSummX * 2)
            * (cellBoundsSummY * 2), Allocator.Temp);

        for (int x = floor.cellBounds.xMin; x < floor.cellBounds.xMax; x++)
        {
            for (int y = floor.cellBounds.yMin; y < floor.cellBounds.yMax; y++)
            {
                //int randomNumber = Random.Range(0, pathNodeArray.Length);
                PathNode pathNode = new PathNode();
                pathNode.x = x;
                pathNode.y = y;
                pathNode.index = CalculateIndex(x, y, cellBoundsSummX, cellBoundsSummY);

                pathNode.gCost = int.MaxValue;
                pathNode.hCost = CalculateDistanceCost(new int2(x, y), endPos);
                pathNode.CalculateFCost();

                pathNode.isWalkable = true;
                pathNode.cameFromNodeIndex = -1;

                pathNodeArray[pathNode.index] = pathNode;
                //Debug.Log(pathNode.x);
                //Debug.Log(pathNode.index);
            }
        }

        NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[]
        {
            new int2(-1,0), //Left
            new int2(1,0),  //Right
            new int2(0,1),  //Up
            new int2(0,-1), //Down
            new int2(-1,-1),//Left Down
            new int2(-1,1), //Left Up
            new int2(1,-1), //Right Down
            new int2(1,1),  //Right Up

        }, Allocator.Temp);


        int endNodeIndex = CalculateIndex(endPos.x, endPos.y, cellBoundsSummX, cellBoundsSummY);
        PathNode startNode = pathNodeArray[CalculateIndex(startPos.x, startPos.y, cellBoundsSummX, cellBoundsSummY)];
        startNode.gCost = 0;
        startNode.CalculateFCost();
        pathNodeArray[startNode.index] = startNode;

        NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        openList.Add(startNode.index);

        while(openList.Length > 0) 
        {
            int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);
            PathNode currentNode = pathNodeArray[currentNodeIndex];

            if (currentNodeIndex == endNodeIndex) 
            {
                // Destination reached...
                break;
            }

            //Romove current node from open list
            for (int i = 0; i < openList.Length; i++)
            {
                if (openList[i] == currentNodeIndex) 
                { 
                    openList.RemoveAtSwapBack(i);
                    break;
                }
            }

            closedList.Add(currentNodeIndex);

            for (int i = 0; i < neighbourOffsetArray.Length; i++)
            {
                int2 neighbourOffset = neighbourOffsetArray[i];
                int2 neighbourPosition = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);

                if (!IsPositionInsideGrid(neighbourPosition)) 
                {
                    // Neighbour not valid position
                    continue;
                }

                int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, cellBoundsSummX, cellBoundsSummY);
                
                if (closedList.Contains(neighbourNodeIndex)) 
                {
                    // Already searched this node
                    continue;
                }

                PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];

                if (!neighbourNode.isWalkable) 
                {
                    //Not walkable
                    continue;
                }

                int2 currentNodePosition = new int2(currentNode.x, currentNode.y);
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbourPosition);
                if(tentativeGCost < neighbourNode.gCost) 
                {
                    neighbourNode.cameFromNodeIndex = currentNodeIndex;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.CalculateFCost();
                    pathNodeArray[neighbourNodeIndex] = neighbourNode;

                    if (!openList.Contains(neighbourNode.index)) 
                    { 
                        openList.Add(neighbourNode.index);
                    }
                }
            }
        }

        PathNode endNode = pathNodeArray[endNodeIndex];
        if (endNode.cameFromNodeIndex == -1) 
        {
            //Didn't find a path!
            Debug.Log("Didn't find a path!");
        }
        else 
        {
            //Found a path
            NativeList<int2> path = CalculatePath(pathNodeArray, endNode);

            foreach(int2 pathPositiopn in path) { Debug.Log(pathPositiopn); }
            
            this.transform.position = new Vector3 (path[0].x + tileAnchor, path[0].y + tileAnchor, 0);
            path.Dispose();
        }

        pathNodeArray.Dispose();
        neighbourOffsetArray.Dispose();
        openList.Dispose();
        closedList.Dispose();
    }

    private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode) 
    { 
        if (endNode.cameFromNodeIndex == -1) 
        {
            //Couldn't find a path!
            return new NativeList<int2>(Allocator.Temp);
        }
        else 
        { 
            //Found a path
            NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
            path.Add(new int2(endNode.x, endNode.y));

            PathNode currentNode = endNode;
            while(currentNode.cameFromNodeIndex != -1) 
            { 
                PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                path.Add(new int2(cameFromNode.x, cameFromNode.y));
                currentNode = cameFromNode;
            }
            return path;
        }
    }

    private bool IsPositionInsideGrid(int2 gridPosition) 
    {
        return
            floor.HasTile(new Vector3Int(gridPosition.x, gridPosition.y));
            //gridPosition.x >= 0 &&
            //gridPosition.y >= 0 &&
            //gridPosition.x < gridSize.x &&
            //gridPosition.y < gridSize.y;
    }

    private int CalculateIndex(int x, int y, int numberX, int numberY) 
    {    
        return ((x + math.abs(numberX)) + (y + math.abs(numberY)) * numberX);
    }

    private int CalculateDistanceCost(int2 aPosition, int2 bPosition) 
    { 
        int xDistance = math.abs(aPosition.x - bPosition.x);
        int yDistance = math.abs(aPosition.y - bPosition.y);
        int remaining = math.abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<PathNode> pathNodeArray) 
    {
        PathNode lowestCostPathNode = pathNodeArray[openList[0]];
        for (int i = 0; i < openList.Length; i++)
        {
            PathNode testPathNode = pathNodeArray[openList[i]];
            if (testPathNode.fCost < lowestCostPathNode.fCost) 
            { 
                lowestCostPathNode = testPathNode;
            }
        }
        return lowestCostPathNode.index;
    }

    private struct PathNode 
    {
        public int x;
        public int y;

        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;

        public int cameFromNodeIndex;

        public void CalculateFCost() 
        {
            fCost = gCost + hCost;
        }
    }
}
