using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoCache
{
    GridManager GM;
    List<Spot> path = new();
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GridManager>();
    }

    public Vector2Int FindNextPosition(Vector2Int destination) 
    { 
        Vector3Int gridPos = GM.tilemap.WorldToCell(transform.position);
        Vector2Int myPosition = new Vector2Int(gridPos.x, gridPos.y);

        if (myPosition == destination) {  }

        path = GM.CreatePath(myPosition, destination);
        //if (path.Count < 2) return Vector2Int.zero;
        return new Vector2Int(path[path.Count].X, path[path.Count].Y);
    }

    public Color markerColor = Color.yellow;
    public float markerSize = 0.2f;

    private void OnDrawGizmosSelected()
    {
        if (GM)
        {
            Gizmos.color = markerColor;
            Grid grid = GM.tilemap.layoutGrid;

            foreach (Spot n in path)
            {
                Vector3 worldPosition = grid.GetCellCenterWorld(new Vector3Int(n.X, n.Y, 0));
                Gizmos.DrawSphere(worldPosition, markerSize);
            }
        }
    }
}
