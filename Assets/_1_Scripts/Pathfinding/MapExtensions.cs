using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapExtensions
{
    public static Vector3Int Up(this Vector3Int vector) { return vector + new Vector3Int(0, 1, 0); }
    public static Vector3Int UpRight(this Vector3Int vector) { return vector + new Vector3Int(1, 1, 0); }
    public static Vector3Int Right(this Vector3Int vector) { return vector + new Vector3Int(1, 0, 0); }
    public static Vector3Int DownRight(this Vector3Int vector) { return vector + new Vector3Int(1, -1, 0); }
    public static Vector3Int Down(this Vector3Int vector) { return vector + new Vector3Int(0, -1, 0); }
    public static Vector3Int DownLeft(this Vector3Int vector) { return vector + new Vector3Int(-1, -1, 0); }
    public static Vector3Int Left(this Vector3Int vector) { return vector + new Vector3Int(-1, 0, 0); }
    public static Vector3Int UpLeft(this Vector3Int vector) { return vector + new Vector3Int(-1, 1, 0); }

    public static Vector3Int[] Neighbours(this Vector3Int vector)
    {
        return new Vector3Int[8]
        {
            vector.Up(),
            vector.UpRight(),
            vector.Right(),
            vector.DownRight(),
            vector.Down(),
            vector.DownLeft(),
            vector.Left(),
            vector.UpLeft()
        };
    }
}
