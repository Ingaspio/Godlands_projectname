using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "SO/TileData")]
public class TileData : ScriptableObject
{
    public TileBase tile;
    public string id;
}
