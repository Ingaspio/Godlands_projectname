using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using System;

public class ItemPlacementHelper : MonoBehaviour
{
    public Tilemap objectsTilemap;
    public GameObject testGameObject;


    public void CreateObjects(IEnumerable<Vector2Int> floorPositions) 
    {
        CreateMultipleObjects(floorPositions, objectsTilemap, testGameObject);
    }

    public void CreateMultipleObjects(IEnumerable<Vector2Int> positions, Tilemap objectsTilemap, GameObject testGameObject)
    {
        foreach (var position in positions) 
        {
            SetObjectPositionToTilePosition(objectsTilemap, testGameObject, position);
        }
    }

    private static void SetObjectPositionToTilePosition(Tilemap tilemap, GameObject testGameObject, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        testGameObject.transform.position = tilePosition;
        Instantiate(testGameObject);
    }
    public void CreateObjectAtlast(Vector2Int position) 
    {
        SetObjectPositionToTilePosition(objectsTilemap,testGameObject,position);
    }

}







//Dictionary<PlacementType, HashSet<Vector2Int>>
//    tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();

//HashSet<Vector2Int> roomFloorNoCorridor;

//public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridor) 
//{ 
//    Graph graph = new Graph(roomFloor);
//    this.roomFloorNoCorridor = roomFloorNoCorridor;

//    foreach (var position in roomFloorNoCorridor)
//    {
//        int neighboursCount8Dir = graph.GetNeighbours8Directions(position).Count;
//        PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

//        if (tileByType.ContainsKey(type) == false)
//            tileByType[type] = new HashSet<Vector2Int>();

//        if (type == PlacementType.NearWall && graph.GetNeighbours4Directions(position).Count < 4)
//            continue;
//        tileByType[type].Add(position);
//    }
//}

//public Vector2? GetItemPlacementPosition(PlacementType placementType, int iterationsMax, Vector2Int positions)
//{
//    int iteration = 0;
//    while (iteration < iterationsMax)
//    {
//        iteration++;
//        int index = Random.Range(0, tileByType[placementType].Count);
//        Vector2Int position = tileByType[placementType].ElementAt(index);

//        tileByType[placementType].Remove(position);
//    }
//    return positions;
//}

//public enum PlacementType { OpenSpace, NearWall }