using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    PathfindMovement movement;
    
    [SerializeField]
    private float testAddvisionAmount;
    [SerializeField]
    private FogOfWarScript fogOfWarScript;

    [SerializeField]
    GameObject playersTP;

    //private void Awake()
    //{
    //    dataFromTiles = new Dictionary<TileBase, TileData>();

    //    foreach (var tileData in tileDatas)
    //    {
    //        foreach (var tile in tileData.tiles)
    //        {
    //            dataFromTiles.Add(tile, tileData);
    //        }
    //    }
    //}

    private void Start()
    {
        fogOfWarScript.PaintFoW();
        fogOfWarScript.PaintFoWExplored();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            TileBase clickedTile = tilemap.GetTile(gridPosition);
        }

        fogOfWarScript.PaintFoWExplored();
        Vector2 playerPosition = new Vector2(playersTP.transform.position.x, playersTP.transform.position.y);
        fogOfWarScript.AddVision(playerPosition);
        movement.PathfindMove();
    }
}
