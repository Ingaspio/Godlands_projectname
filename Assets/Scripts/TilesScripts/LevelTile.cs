using System;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level tile", menuName = "2D/Tiles/LevelTile")]
public class LevelTile : Tile
{
    public TileBase tile;
    public Vector3Int position;
}

public enum TileType 
{
    fowExploredTile=0, 
    fowTile = 1, 
    floorTile=2, 
    wallTop=3, 
    wallSideRight=4, 
    wallSideLeft=5, 
    wallBottom=6, 
    wallFull=7, 
    wallInnerCornerDownLeft=8, 
    wallInnerCornerDownRight=9,
    wallDiagonalCornerDownRight=10, 
    wallDiagonalCornerDownLeft=11, 
    wallDiagonalCornerUpRight=12, 
    wallDiagonalCornerUpLeft=13,


}
