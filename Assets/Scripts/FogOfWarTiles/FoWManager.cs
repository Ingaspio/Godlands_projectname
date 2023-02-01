using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;
using System.Collections;

public class FoWManager : MonoBehaviour
{
    [SerializeField]
    Tilemap fogOfWarTilemap, fogOfWarTilemapExplored;
    [SerializeField]
    FogOfWarVisualizer fogOfWarVisualizer;
    private Dictionary<Vector3Int, float> visibleTiles = new();
    [SerializeField]
    float maxVisability = 1f;
    [SerializeField]
    private Color maxVisabilityColor, minVisabilityColor, foggyColor;
    [SerializeField]
    private float visionFallOff, reduceAmount, reduceInterwall = 1f;
    [SerializeField]
    public int testRadius;

    private void ChangeVisableTiles(Vector3Int gridPosition, float changeBy) 
    {
        if (!visibleTiles.ContainsKey(gridPosition))
            visibleTiles.Add(gridPosition, 0f);

        float newValue = visibleTiles[gridPosition] + changeBy;

        if (newValue <= 0f) 
        {
            visibleTiles.Remove(gridPosition);

            fogOfWarTilemapExplored.SetTileFlags(gridPosition, TileFlags.None);
            fogOfWarTilemapExplored.SetColor(gridPosition, foggyColor);
            fogOfWarTilemapExplored.SetTileFlags(gridPosition, TileFlags.LockColor);
        }
        else visibleTiles[gridPosition] = Mathf.Clamp(newValue, 0f, maxVisability);
    }

    public IEnumerator ReduceVisabilityRoutine()
    {
        Dictionary<Vector3Int, float> visibleTilesCopy = new Dictionary<Vector3Int, float>(visibleTiles);

        while (true)
        {
            foreach (var entry in visibleTilesCopy)
            {
                ChangeVisableTiles(entry.Key, reduceAmount);
            }
            yield return new WaitForSeconds(reduceInterwall);
            VisualizeVisability();
        }
    }

    private void VisualizeVisability() 
    {
        foreach (var entry in visibleTiles)
        {
            float visabilityPercent = entry.Value / maxVisability;

            Color newTileColor = maxVisabilityColor * visabilityPercent + minVisabilityColor * (1f - visabilityPercent);

            fogOfWarTilemapExplored.SetTileFlags(entry.Key, TileFlags.None);
            fogOfWarTilemapExplored.SetColor(entry.Key, newTileColor);
            fogOfWarTilemapExplored.SetTileFlags(entry.Key, TileFlags.LockColor);
        }
    }

    public void AddVision(Vector2 worldPosition, float visionAmount, int radius) 
    {
        Vector3Int gridPosition = fogOfWarTilemapExplored.WorldToCell(worldPosition);
        //fogOfWarVisualizer.ClearSingleFoWTile(fogOfWarTilemap, gridPosition);
        //fogOfWarVisualizer.ClearSingleFoWTile(fogOfWarTilemapExplored, gridPosition);
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if (distanceFromCenter <= radius)
                {
                    Vector3Int nextTilePosition = new Vector3Int(gridPosition.x + x, gridPosition.y + y, 0);
                    ChangeVisableTiles(nextTilePosition, visionAmount - (distanceFromCenter * visionFallOff * visionAmount));
                    fogOfWarVisualizer.ClearSingleFoWTile(fogOfWarTilemap, nextTilePosition);
                    //fogOfWarVisualizer.ClearSingleFoWTile(fogOfWarTilemapExplored, nextTilePosition);
                }
            }
        }
        ChangeVisableTiles(gridPosition, visionAmount);
        VisualizeVisability();
    }
    
}
