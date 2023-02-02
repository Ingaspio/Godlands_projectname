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
    public void Vector3IntClearSingleTile(Tilemap fowTilemap, Vector3Int position) 
    {
        var fowTilePosition = fowTilemap.WorldToCell((Vector3)position); 
        fowTilemap.SetTile(fowTilePosition, null);
    }
    public void Vector2ClearSingleTile(Tilemap fowTilemap, Vector2Int position)
    {
        var fowTilePosition = fowTilemap.WorldToCell((Vector2)position);
        fowTilemap.SetTile(fowTilePosition, null);
    }
    private void PaintFoWTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleFoWTile(tilemap, tile, position);
        }
    }
    public void ClearFoWTiles(IEnumerable<Vector2Int> fieldOfView, Tilemap fowTilemap)
    {
        foreach (var position in fieldOfView)
        {
            Vector2ClearSingleTile(fowTilemap, position);
        }
    }
    public void PaintFoWTilesInWorld(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowTilemap, fowTileBase);
    }
    public void PaintFoWExploredTilesInWorld(IEnumerable<Vector2Int> fowPositions)
    {
        PaintFoWTiles(fowPositions, fowExploredTilemap, fowExploredTileBase);
    }
    public void ClearFoWTilesInWorld(IEnumerable<Vector2Int> fowPositions)
    {
        ClearFoWTiles(fowPositions, fowTilemap);
    }


}