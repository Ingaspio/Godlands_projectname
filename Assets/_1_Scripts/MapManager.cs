using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Collections;

public class MapManager : MonoCache
{
    [SerializeField]
    Tilemap floor, fogOfWar;
    public static MapManager mainMap;
    FogOfWarScript fog;
    private void Start()
    {
        if (mainMap == null) mainMap = this; else throw new System.Exception("There's can be only one map manager");
        fog = GameObject.Find("FoWManager").GetComponent<FogOfWarScript>();
        fog.PaintFoW();
        ObjectsPlacementOnTiles objectPlacement = FindObjectOfType<ObjectsPlacementOnTiles>();
        objectPlacement.PlaceObjects();
        StartCoroutine(WaitForSecondToPaintFoWExplored());
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
            if (floor.HasTile(neighbour) && floor.GetTile<CustomTile>(neighbour).isWalkable && !fogOfWar.HasTile(neighbour))
                result.Add(neighbour, 1);
        }
        return result;
    }
    IEnumerator WaitForSecondToPaintFoWExplored() 
    {
        while (true)
        {
            if (PlayerCharacter.instance.transform.hasChanged)
            {
                fog.PaintFoWExplored();
                yield return new WaitForSeconds(3);
            }
        }
    }
}
