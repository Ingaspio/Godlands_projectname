using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Linq;

public class PathGizmosDrawer : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int startCell;
    public Vector3Int endCell;
    bool bothEndsAssiged = false;
    List<Vector3Int> path;
    UltimatePathfinding<Vector3Int> pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        //tilemap = GameObject.Find("Floor").GetComponent<Tilemap>();
        pathfinder = new UltimatePathfinding<Vector3Int>();
        pathfinder.GetHeuristicDistance = (l, r) => GetManhattanDistance(l, r);
        pathfinder.GetNeighborsAndStepCosts = (x) => GetNeighboursAndCosts(x);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (bothEndsAssiged) 
            { 
                startCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                startCell.z = 0;
            }
            else 
            {
                endCell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                endCell.z = 0;
                if (pathfinder.GeneratePath(startCell, endCell, out path))
                    path.Insert(0, startCell);
            }
            bothEndsAssiged = !bothEndsAssiged;
        }
        if (bothEndsAssiged && path.Count > 0)
            DrawPathGizmo();
    }

    void DrawPathGizmo()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(tilemap.CellToWorld(path[i]) + new Vector3(0.5f, 0.5f, 0f), tilemap.CellToWorld(path[i + 1]) + new Vector3(0.5f, 0.5f, 0f));
        }

    }
    public float GetManhattanDistance(Vector3Int A, Vector3Int B) 
    { 
        return Mathf.Abs(A.x-B.x) + Mathf.Abs(A.y-B.y);
    }

    public Dictionary<Vector3Int, float> GetNeighboursAndCosts(Vector3Int pos)
    {
        Dictionary<Vector3Int, float> result = new Dictionary<Vector3Int, float>();
        foreach (Vector3Int neighbour in pos.Neighbours())
        {
            if (tilemap.HasTile(neighbour) && tilemap.GetTile<CustomTile>(neighbour).isWalkable)
                result.Add(neighbour, 1);
        }
        return result;
    }
}
