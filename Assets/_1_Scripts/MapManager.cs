using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MapManager : MonoCache
{
    [SerializeField]
    Tilemap floor;
    public static MapManager mainMap;
    
    private void Start()
    {
        if (mainMap == null) mainMap = this; else throw new System.Exception("There's can be only one map manager");

        ObjectsPlacementOnTiles objectPlacement = FindObjectOfType<ObjectsPlacementOnTiles>();
        objectPlacement.PlaceObjects();
    }

    public Vector3Int WorldToCell(Vector3 worldPosition) 
    {
        Vector3Int cell = floor.WorldToCell(worldPosition);
        cell.z = 0;
        return cell;
    }
    public Vector3 CellToWorld(Vector3Int cellPosition) 
    { 
        return floor.CellToWorld(cellPosition);
    }
    public int GetMovementCost(Vector3Int tilePos, int movementBonus) 
    { 
        return floor.GetTile<CustomTile>(tilePos).baseWalkSpeed + movementBonus;
    }
    public float GetManhattanDistance(Vector3Int A, Vector3Int B)
    {
        return Mathf.Abs(A.x - B.x) + Mathf.Abs(A.y - B.y);
    }

    public Dictionary<Vector3Int, float> GetNeighboursAndCosts(Vector3Int pos)
    {
        Dictionary<Vector3Int, float> result = new Dictionary<Vector3Int, float>();
        foreach (Vector3Int neighbour in pos.Neighbours())
        {
            if (floor.HasTile(neighbour) /*&& tilemap.GetTile<CustomTile>(neighbour).isWalkable*/)
                result.Add(neighbour, 1);
        }
        return result;
    }
}
