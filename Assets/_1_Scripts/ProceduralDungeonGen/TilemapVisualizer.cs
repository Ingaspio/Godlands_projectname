using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, fowTilemap, fowExploredTilemap;
    [SerializeField]
    private TileData fowExploredTile, fowTile, floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornerDownLeft, wallInnerCornerDownRight, 
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

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileData tileData)
    {
        foreach (var position in positions) 
        {
            PaintSingleTile(tilemap, tileData.tileBase, position);
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
        TileData tileData = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt)) { tileData = wallTop; }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt)) { tileData = wallSideRight; }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt)) { tileData = wallBottom; }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt)) { tileData = wallSideLeft; }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt)) { tileData = wallFull; }
        if (tileData != null)
            PaintSingleTile(wallTilemap, tileData.tileBase, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileData tileData = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) { tileData = wallInnerCornerDownLeft; }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)) { tileData = wallInnerCornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) { tileData = wallDiagonalCornerDownRight; }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) { tileData = wallDiagonalCornerDownLeft; }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) { tileData = wallDiagonalCornerUpRight; }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) { tileData = wallDiagonalCornerUpLeft; }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt)) { tileData = wallFull; }
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt)) { tileData = wallBottom; }
        if (tileData != null)
            PaintSingleTile(wallTilemap, tileData.tileBase, position);
    }
}
