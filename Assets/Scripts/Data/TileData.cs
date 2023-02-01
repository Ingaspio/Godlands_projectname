using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public int tileValueInt = 0;
    public float tileValueFloat = 0f;
}
