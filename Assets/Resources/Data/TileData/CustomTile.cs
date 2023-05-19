using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "CustomTiles/CustomTile")]
public class CustomTile : Tile
{
    public TileBase tileBase;
    public string id;
    public bool isWalkable;
    public int baseWalkSpeed;
}
