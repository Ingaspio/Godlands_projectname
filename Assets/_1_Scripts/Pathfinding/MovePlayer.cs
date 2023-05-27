using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MovePlayer : MonoCache
{
    UltimatePathfinding<Vector3Int> pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = new UltimatePathfinding<Vector3Int>();
        pathfinder.GetHeuristicDistance = (x,y) => MapManager.mainMap.GetManhattanDistance(x,y);
        pathfinder.GetNeighborsAndStepCosts = (x) => MapManager.mainMap.GetNeighboursAndCosts(x);
    }

    // Update is called once per frame
    public override void OnTick()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3Int clickedOnCell = MapManager.mainMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            List<Vector3Int> path;
            if (pathfinder.GeneratePath(PlayerCharacter.instance.GetVector3IntPlayerPosition(), clickedOnCell, out path)) 
            { 
                PlayerCharacter.instance.path = path;
                PlayerCharacter.instance.StopAllCoroutines();
                PlayerCharacter.instance.StartCoroutine(PlayerCharacter.instance.followThePath(0.05f));
            }
        }
    }
}
