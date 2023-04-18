
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineOfSight : MonoCache
{
    public Transform player;
    public Tilemap tilemap;
    public TileData[] obstacleTiles;
    public float viewDistance;

    public override void OnTick()
    {
        Vector3Int startCell = tilemap.WorldToCell(player.position);
        Vector3Int endCell = tilemap.WorldToCell(transform.position);
        Vector3 direction = endCell - startCell;
        direction.Normalize();
        int distance = Mathf.RoundToInt(Vector3.Distance(startCell, endCell));
        
        for (int i = 0; i <= distance; i++)
        {
            Vector3Int cell = startCell + new Vector3Int (Mathf.RoundToInt(direction.x * i), Mathf.RoundToInt(direction.y * i));

            if (CheckTileForObstacle(cell))
            {
                // obstacle is visible, do something
            }
            else
            {
                // obstacle is hidden by another obstacle
            }
        }
    }

    bool CheckTileForObstacle(Vector3Int cell)
    {
        TileBase tile = tilemap.GetTile(cell);

        if (tile == null)
            return false;

        foreach (TileData obstacleTile in obstacleTiles)
        {
            if (tile == obstacleTile)
                return true;
        }

        return false;
    }
    
}

// од берет координаты начальной и конечной €чеек, на которых наход€тс€ игрок и цель.
//«атем он проходит по всем €чейкам между ними, провер€€ каждую на наличие преп€тстви€.
//ƒл€ проверки тайла на преп€тствие используетс€ метод CheckTileForObstacle, который ищет совпадение тайла с каждым заданным в массиве obstacleTiles.
//≈сли совпадение найдено, то преп€тствие видимо.

