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
    [SerializeField]
    private FogOfWarScript fogOfWarScript;
    
    
    
    private void Start()
    {
        
        
        ObjectsPlacementOnTiles objectPlacement = FindObjectOfType<ObjectsPlacementOnTiles>();
        objectPlacement.PlaceObjects();
        

        fogOfWarScript.PaintFoW();
        fogOfWarScript.PaintFoWExplored();

        

    }

    public override void OnTick()
    { 
        fogOfWarScript.PaintFoWExplored();

        Vector2 playerPosition = new Vector2(PlayerCharacter.instance.transform.position.x, PlayerCharacter.instance.transform.position.y);
        fogOfWarScript.AddVision(playerPosition);
       
    }
}
