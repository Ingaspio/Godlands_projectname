using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BresenhamAlgorythm
{
    public HashSet<Vector2Int> BresenhamsLineAlgorythm(Vector2Int start, Vector2Int end)
    {
        HashSet<Vector2Int> visionLine = new HashSet<Vector2Int>();
        Tilemap walls = GameObject.Find("Walls").GetComponent<Tilemap>();

        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);
        int sx = (start.x < end.x) ? 1 : -1;
        int sy = (start.y < end.y) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (start.x == end.x && start.y == end.y)
                break;
            if (walls.HasTile(new Vector3Int(start.x, start.y, 0)))
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                start.x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                start.y += sy;
            }
            visionLine.Add(start);
        }
        return visionLine;
    }
}
