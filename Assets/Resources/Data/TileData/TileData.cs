using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "SO/TileData")]
public class TileData : RuleTile
{
    public TileBase tileBase;
    public string id;
    public bool isWalkable;
}
