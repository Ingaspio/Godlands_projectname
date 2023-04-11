using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BerrensteinsAlgTestVision : MonoCache
{
    [SerializeField]
    public TilemapVisualizer tilemapVisualizer;
    [SerializeField]
    Tilemap walls, fogOfWarExplored, fogOfWar, floorTilemap;
    public int vision = 3;
    public Vector3 tileAnchor;
    List<Vector3> wallsPositions = new List<Vector3>();
    float timer = 0f;
    private float updateTimer = 1f;
    public HashSet<Vector2Int> FoWVisionPlayerDiamond()
    {  
        int visionShrink = 0;
        int visionFloored = (int)Mathf.Floor(vision/2);
        int playerPosY = (int)this.transform.position.y;
        int playerPosX = (int)this.transform.position.x;        
       
        HashSet<Vector2Int> playerVisionComplete = new();

        Vector2Int playerPosVector2Int = new Vector2Int(playerPosX, playerPosY);
        playerVisionComplete.Add(playerPosVector2Int);

        Vector2Int endPosXPlus = new Vector2Int(playerPosX + vision, playerPosY);
        Vector2Int endPosXMinus = new Vector2Int(playerPosX - vision, playerPosY);
        Vector2Int endPosYPlus = new Vector2Int(playerPosX, playerPosY + vision);
        Vector2Int endPosYMinus = new Vector2Int(playerPosX, playerPosY - vision);
        HashSet<Vector2Int> visionLineXPlus = BresenhamsAlgorithm(playerPosVector2Int, endPosXPlus);
        HashSet<Vector2Int> visionLineXMinus = BresenhamsAlgorithm(playerPosVector2Int, endPosXMinus);
        HashSet<Vector2Int> visionLineYPlus = BresenhamsAlgorithm(playerPosVector2Int, endPosYPlus);
        HashSet<Vector2Int> visionLineYMinus = BresenhamsAlgorithm(playerPosVector2Int, endPosYMinus);

        Vector2Int endPosDiagRightUp = new Vector2Int(playerPosX + visionFloored, playerPosY + visionFloored);
        Vector2Int endPosDiagRightDown = new Vector2Int(playerPosX + visionFloored, playerPosY - visionFloored);
        Vector2Int endPosDiagLeftDown = new Vector2Int(playerPosX - visionFloored, playerPosY - visionFloored);
        Vector2Int endPosDiagLeftUp = new Vector2Int(playerPosX - visionFloored, playerPosY + visionFloored);
        HashSet<Vector2Int> visionLineDiagRightUp = BresenhamsAlgorithm(playerPosVector2Int, endPosDiagRightUp);
        HashSet<Vector2Int> visionLineDiagRightDown = BresenhamsAlgorithm(playerPosVector2Int, endPosDiagRightDown);
        HashSet<Vector2Int> visionLineDiagLeftDown = BresenhamsAlgorithm(playerPosVector2Int, endPosDiagLeftDown);
        HashSet<Vector2Int> visionLineDiagLeftUp = BresenhamsAlgorithm(playerPosVector2Int, endPosDiagLeftUp);

        playerVisionComplete.UnionWith(visionLineXPlus);
        playerVisionComplete.UnionWith(visionLineXMinus);
        playerVisionComplete.UnionWith(visionLineYPlus);
        playerVisionComplete.UnionWith(visionLineYMinus);
        playerVisionComplete.UnionWith(visionLineDiagRightUp);
        playerVisionComplete.UnionWith(visionLineDiagRightDown);
        playerVisionComplete.UnionWith(visionLineDiagLeftDown);
        playerVisionComplete.UnionWith(visionLineDiagLeftUp);

        for (int t = 1; t <= visionFloored; t++)
        {
            visionShrink += 2;

            Vector2Int startDiagRightUp = new Vector2Int(playerPosX + t, playerPosY + t);
            Vector2Int startPosDiagRightDown = new Vector2Int(playerPosX + t, playerPosY - t);
            Vector2Int startPosDiagLeftDown = new Vector2Int(playerPosX - t, playerPosY - t);
            Vector2Int startPosDiagLeftUp = new Vector2Int(playerPosX - t, playerPosY + t);

            HashSet<Vector2Int> visionLineRightUpY = BresenhamsAlgorithm(startDiagRightUp, new Vector2Int(startDiagRightUp.x, startDiagRightUp.y + vision - visionShrink));
            HashSet<Vector2Int> visionLineRightUpX = BresenhamsAlgorithm(startDiagRightUp, new Vector2Int(startDiagRightUp.x + vision - visionShrink, startDiagRightUp.y));

            HashSet<Vector2Int> visionLineRightDownY = BresenhamsAlgorithm(startPosDiagRightDown, new Vector2Int(startPosDiagRightDown.x, startPosDiagRightDown.y - vision + visionShrink));
            HashSet<Vector2Int> visionLineRightDownX = BresenhamsAlgorithm(startPosDiagRightDown, new Vector2Int(startPosDiagRightDown.x + vision - visionShrink, startPosDiagRightDown.y));

            HashSet<Vector2Int> visionLeftDownY = BresenhamsAlgorithm(startPosDiagLeftDown, new Vector2Int(startPosDiagLeftDown.x, startPosDiagLeftDown.y - vision + visionShrink));
            HashSet<Vector2Int> visionLeftDownX = BresenhamsAlgorithm(startPosDiagLeftDown, new Vector2Int(startPosDiagLeftDown.x - vision + visionShrink, startPosDiagLeftDown.y));

            HashSet<Vector2Int> visionLineLeftUpY = BresenhamsAlgorithm(startPosDiagLeftUp, new Vector2Int(startPosDiagLeftUp.x, startPosDiagLeftUp.y + vision - visionShrink));
            HashSet<Vector2Int> visionLineLeftUpX = BresenhamsAlgorithm(startPosDiagLeftUp, new Vector2Int(startPosDiagLeftUp.x - vision + visionShrink, startPosDiagLeftUp.y));

            playerVisionComplete.Add(startDiagRightUp);
            playerVisionComplete.Add(startPosDiagRightDown);
            playerVisionComplete.Add(startPosDiagLeftDown);
            playerVisionComplete.Add(startPosDiagLeftUp);

            playerVisionComplete.UnionWith(visionLineRightUpY);
            playerVisionComplete.UnionWith(visionLineRightUpX);
            playerVisionComplete.UnionWith(visionLineRightDownY);
            playerVisionComplete.UnionWith(visionLineRightDownX);
            playerVisionComplete.UnionWith(visionLeftDownY);
            playerVisionComplete.UnionWith(visionLeftDownX); 
            playerVisionComplete.UnionWith(visionLineLeftUpY);
            playerVisionComplete.UnionWith(visionLineLeftUpX);
        }
        

        return playerVisionComplete;
    }

    public HashSet<Vector2Int> BresenhamsAlgorithm(Vector2Int start, Vector2Int end)
    {
        HashSet<Vector2Int> visionLine = new HashSet<Vector2Int>();
        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);
        int sx = (start.x < end.x) ? 1 : -1;
        int sy = (start.y < end.y) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            //foreach (Vector3 pos in wallsPositions)
            //{
            //    Vector2Int wallPos2Int = new Vector2Int((int)pos.x, (int)pos.y);
            //    if (visionLine.Contains(wallPos2Int))
            //    {
            //        Debug.Log(pos);
            //    }
            //}
            //Debug.Log("Point: " + start);
            if (start.x == end.x && start.y == end.y)
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

    protected void FindWallsPositions()
    {
        for (int n = walls.cellBounds.xMin; n < walls.cellBounds.xMax; n++)
        {
            for (int p = walls.cellBounds.yMin; p < walls.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)walls.transform.position.y);
                Vector3 place = walls.CellToWorld(localPlace);
                if (walls.HasTile(localPlace))
                {
                    //Tile at "place"
                    wallsPositions.Add(place + tileAnchor);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    public void AddVision(Vector2 playerTP)
    {
        HashSet<Vector2Int> playerVisionUp = FoWVisionPlayerDiamond();
        tilemapVisualizer.ClearTiles(playerVisionUp, fogOfWar);
        tilemapVisualizer.ClearTiles(playerVisionUp, fogOfWarExplored);
    }
    private void Awake()
    {
        FindWallsPositions();
    }
    public override void OnTick() 
    {
        Vector2 playerPosition = new Vector2(PlayerCharacter.instance.transform.position.x, PlayerCharacter.instance.transform.position.y-1);
        timer += Time.deltaTime;
        if (timer >= updateTimer)
        {
            AddVision(playerPosition);
            timer = 0;
        }
    }
}

