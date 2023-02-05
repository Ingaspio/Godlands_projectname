using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ObjectsPlacementOnTiles : MonoBehaviour
{
    public Tilemap tileMap;
    public Vector3 tileAnchor;
    public GameObject prefab;
    [HideInInspector]
    public List<Vector3> availablePlaces;
    [SerializeField]
    FogOfWarScript fogOfWarScript;
    
    void Start()
    {
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place + tileAnchor);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        int randomElement = Random.Range(0, availablePlaces.Count + 1);
        Debug.Log("Random range element: " + randomElement + ", coordinates: " + availablePlaces[randomElement]);
        prefab.transform.position = availablePlaces[randomElement];
        Instantiate(prefab);
    }
    
}
    
    












//public static void CreateObjectInRandomPosition(HashSet<Vector2Int> floorPositions, ItemPlacementHelper itemPlacementHelper) 
//{
//    var objectPositions = FindObjectsInPositions(floorPositions, Direction2D.cardinalDirectionsList);
//    foreach (var position in objectPositions) 
//    {
//        itemPlacementHelper.CreateObjectAtlast(position);
//    }
//}

//private static HashSet<Vector2Int> FindObjectsInPositions(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
//{
//    HashSet<Vector2Int> randomPositions = new HashSet<Vector2Int>();
//    foreach (var position in floorPositions)
//    {
//        foreach (var direction in directionList)
//        {
//            var neighbourPosition = position + direction;
//            if (floorPositions.Contains(neighbourPosition) == true) 
//            { 
//                randomPositions.Add(neighbourPosition);
//            }
//        }
//    }
//    return randomPositions;
//}