using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoCache
{
    public Tilemap tilemap;
    BoundsInt bounds;
    public Vector3Int[,] spots;
    Astar astar;
    public int maxSteps = 1000;
    
    // Start is called before the first frame update
    void Start()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }
    public void CreateGrid()
    {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                Vector3Int myGridPos = new Vector3Int(x, y, 0);
                TileBase myTile = tilemap.GetTile(myGridPos);
                
                if (myTile && myTile is CustomTile && ((CustomTile)myTile) == true)
                {
                    spots[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
        
    }
    public List<Spot> CreatePath(Vector2Int startCell, Vector2Int endCell) 
    {
        return astar.CreatePath(spots, startCell, endCell, maxSteps);
    }
    public List<Vector3> CreatePathV3(List<Spot> listNodes) 
    { 
        List<Vector3> result = new List<Vector3>();
        foreach (Spot node in listNodes) 
        {
            if (node != null)
            {
                Vector3 vector3 = new Vector3(node.X, node.Y, 0);
                result.Add(vector3);
            }
            else { break; }
        }
        
        return result;
    }
}
