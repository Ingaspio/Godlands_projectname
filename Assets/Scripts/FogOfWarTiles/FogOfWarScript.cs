using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FogOfWarScript : MonoBehaviour
{
    public int offset = 35;
    public FogOfWarVisualizer fogOfWarVisualizer;
    [SerializeField]
    Tilemap fogOfWarExplored;
    [SerializeField]
    protected Vector2Int fowSizeInt = new Vector2Int(50, 50);

    public void PaintFoW()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWTilesInWorld(fowTiles);
    }
    public void PaintFoWExplored()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWExploredTilesInWorld(fowTiles);
    }
    public HashSet<Vector2Int> FoWTilesCreation(Vector2Int fowSize)
    {

        HashSet<Vector2Int> fowTiles = new();
        for (int x = 0 - offset; x < fowSize.x; x++)
        {
            for (int y = 0 - offset; y < fowSize.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                fowTiles.Add(position);
            }

        }
        return fowTiles;
    }
    public HashSet<Vector2Int> FoWVisionTiles(Vector2 playerPosition, int radius)
    {
        HashSet<Vector2Int> playerVision = new();
        Vector3Int gridPosition = fogOfWarExplored.WorldToCell(playerPosition);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if (distanceFromCenter <= radius)
                {
                    Vector2Int nextTilePosition = new Vector2Int(gridPosition.x + x, gridPosition.y + y);
                    playerVision.Add(nextTilePosition);
                }
            }
        }
        return playerVision;
    }
}