using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MapManager : MonoCache
{
    [SerializeField]
    Tilemap tilemap;
    //[SerializeField]
    //EntranceRoutine entranceRoutine;    
    [SerializeField]
    private float testAddvisionAmount;
    
    
    
    
    private void Start()
    {      
        ObjectsPlacementOnTiles objectPlacement = FindObjectOfType<ObjectsPlacementOnTiles>();
        objectPlacement.PlaceObjects();
    }

}
