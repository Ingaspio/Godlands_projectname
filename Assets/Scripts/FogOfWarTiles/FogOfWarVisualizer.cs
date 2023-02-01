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
    public void ClearSingleFoWTile(Tilemap fowTilemap, Vector3Int position) 
    {
        var fowTilePosition = fowTilemap.WorldToCell((Vector3)position); 
        fowTilemap.SetTile(fowTilePosition, null);
    }
    private void PaintFoWTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleFoWTile(tilemap, tile, position);
        }
    }
    //public void ClearFoWTiles(IEnumerable<Vector2> fieldOfView, Tilemap fowTilemap)
    //{
    //    foreach (var position in fieldOfView)
    //    {
    //        ClearSinleFoWTile(fowTilemap,  position);
    //    }
    //}
    public void PaintFoWTilesInWorld(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowTilemap, fowTileBase);
    }
    public void PaintFoWExploredTilesInWorld(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowExploredTilemap, fowExploredTileBase);
    }
    //public void ClearFoWTilesInWorld(IEnumerable<Vector2> fowPositions) 
    //{
    //    ClearFoWTiles(fowPositions, fowTilemap);
    //}

    
}