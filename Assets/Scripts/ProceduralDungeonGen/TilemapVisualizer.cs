using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, fowTilemap, fowExploredTilemap;
    [SerializeField]
    private TileBase fowExploredTile, fowTile, floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }
    public void PaintFoWTiles(IEnumerable<Vector2Int> fowPosition) 
    {
        PaintTiles(fowPosition, fowTilemap, fowTile);
    }
    public void PaintFoWExploredTiles(IEnumerable<Vector2Int> fowPosition)
    {
        PaintTiles(fowPosition, fowExploredTilemap, fowExploredTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions) 
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }


    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    private void ClearSingleTile(Tilemap tilemap, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, null);
    }

    public void ClearTiles(IEnumerable<Vector2Int> fieldOfView, Tilemap fowTilemap)
    {
        foreach (var position in fieldOfView)
        {
            ClearSingleTile(fowTilemap, position);
        }
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt)) { tile = wallTop; }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt)) { tile = wallSideRight; }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt)) { tile = wallBottom; }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt)) { tile = wallSideLeft; }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt)) { tile = wallFull; }
        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) { tile = wallInnerCornerDownLeft; }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) { tile = wallInnerCornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) { tile = wallDiagonalCornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) { tile = wallDiagonalCornerDownLeft; }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) { tile = wallDiagonalCornerUpRight; }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) { tile = wallDiagonalCornerUpLeft; }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)) { tile = wallFull; }
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt)) { tile = wallBottom; }
        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }
}