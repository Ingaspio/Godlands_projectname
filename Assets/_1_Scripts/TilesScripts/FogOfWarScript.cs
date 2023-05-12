using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FogOfWarScript : MonoBehaviour
{
    [SerializeField]
    public TilemapVisualizer tilemapVisualizer;
    [SerializeField]
    Tilemap fogOfWar, fogOfWarExplored, floorTilemap;
    [SerializeField]
    public int testRadius;
    

    public void PaintFoW()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation();
        tilemapVisualizer.PaintFoWTiles(fowTiles);
    }
    public void PaintFoWExplored()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation();
        tilemapVisualizer.PaintFoWExploredTiles(fowTiles);
    }
    public HashSet<Vector2Int> FoWTilesCreation()
    {
        HashSet<Vector2Int> fowTiles = new();
        for (int x = floorTilemap.cellBounds.xMin - 10; x < floorTilemap.cellBounds.xMax + 10; x++)
        {
            for (int y = floorTilemap.cellBounds.yMin - 10; y < floorTilemap.cellBounds.yMax + 10; y++)
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
