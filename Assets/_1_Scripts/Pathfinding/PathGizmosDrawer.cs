using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Linq;
using Unity.VisualScripting;

public class PathGizmosDrawer : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int startCell;
    public Vector3Int endCell;
    //bool bothEndsAssiged = false;
    List<Vector3Int> path;
    UltimatePathfinding<Vector3Int> pathfinder;
    public int clickTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        //tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
        pathfinder = new UltimatePathfinding<Vector3Int>();
        pathfinder.GetHeuristicDistance = (l, r) => MapManager.mainMap.GetManhattanDistance(l, r);
        pathfinder.GetNeighborsAndStepCosts = (x) => MapManager.mainMap.GetNeighboursAndCosts(x); 
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            startCell = new Vector3Int((int)PlayerCharacter.instance.transform.position.x, (int)PlayerCharacter.instance.transform.position.y, 0);
            startCell.z = 0;
            clickTimes++;
            if (Input.GetMouseButtonDown(1))
                clickTimes = 0;
            if (clickTimes == 1) 
            { 
                endCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                endCell.z = 0;

                if (pathfinder.GeneratePath(startCell, endCell, out path))
                {
                    path.Insert(0, startCell);
                    Debug.Log(path.Count);
                }
            }
            if (path.Count > 0 && clickTimes == 2)
                DrawPathGizmo();
            if (path.Count > 0 && clickTimes == 3)
            {
                while(transform.position != tilemap.CellToWorld(path[0]))
                    transform.position = Vector3.MoveTowards(transform.position, tilemap.CellToWorld(path[0]), 0.5f);
                clickTimes = 0;

            }
        }
    }

    void DrawPathGizmo()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(tilemap.CellToWorld(path[i]) + new Vector3(0.5f, 0.5f, 0f), tilemap.CellToWorld(path[i + 1]) + new Vector3(0.5f, 0.5f, 0f));
        }

    }
    
}
