using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWarVisualizer : MonoBehaviour 
{
    [SerializeField]
    private Tilemap fowTilemap, fowExploredTilemap;
    [SerializeField]
    private TileBase fowTileBase, fowExploredTileBase;

    private void PaintSingleFoWTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    private void PaintFoWTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleFoWTile(tilemap, tile, position);
        }
    }
    public void PaintFoWTiles(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowTilemap, fowTileBase);
    }
    public void PaintFoWExploredTiles(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowExploredTilemap, fowExploredTileBase);
    }
}



    
    
    
    
    
    
    
    
    
    
    
    
    
    //public static HashSet<Vector2Int> PlayerVisionGenerator(Vector2Int playerPosition, int visionLength)
    //{
    //    HashSet<Vector2Int> fieldOfView = new HashSet<Vector2Int>();

    //    fieldOfView.Add(playerPosition);
    //    var previousPosition = playerPosition;

    //    for (int i = 0; i < visionLength; i++)
    //    {
    //        var newPosition = previousPosition + Direction2D.eightDirectionsList[i];
    //        fieldOfView.Add(newPosition);
    //        previousPosition = newPosition;
    //    }
    //    return fieldOfView;
    //}


    //public void PlayerVisionPainter() 
    //{
    //    int xRound = Mathf.RoundToInt(playersTP.transform.position.x);
    //    int yRound = Mathf.RoundToInt(playersTP.transform.position.y);
    //    Vector2Int playerPosition = new Vector2Int(xRound, yRound);
    //    HashSet<Vector2Int> playersVision = PlayerVisionGenerator(playerPosition, 4);
        
    //}
        

    

