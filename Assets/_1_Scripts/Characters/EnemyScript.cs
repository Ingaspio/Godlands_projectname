using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoCache
{
    public EnemyStatsSO enemyStats;
    private float timer = 0f;
    private float updateTimer = 3f;

    //UnionWith for combine two HashSets

    public HashSet<Vector2Int> FoWVisionTiles(EnemyStatsSO enemyStats)
    {
        int radiusShrink = 1;
        int enemyPosY = (int)this.transform.position.y;
        int enemyPosX = (int)this.transform.position.x;
        int enemyRadius = enemyStats.visionRadius;

        Vector2Int enemyPositionVector = new Vector2Int(enemyPosX, enemyPosY);
        Vector2Int enemyVisionXPlus = new Vector2Int(enemyPosX + enemyRadius - radiusShrink, enemyPosY);
        Vector2Int enemyVisionXMinus = new Vector2Int(enemyPosX - enemyRadius + radiusShrink, enemyPosY);
        HashSet<Vector2Int> visionLineXPlus = BresenhamsAlgorithm(enemyPositionVector, enemyVisionXPlus);
        HashSet<Vector2Int> visionLineXMinus = BresenhamsAlgorithm(enemyPositionVector, enemyVisionXMinus);
        HashSet<Vector2Int> enemyVision = new();
        
        for (int t = 0; t <= enemyRadius; t++)
        {
            radiusShrink++;
            enemyPosY++;
            visionLineXPlus.UnionWith(visionLineXMinus);
        }
        for (int i = 0; i <= enemyRadius ; i++)
        {
            radiusShrink++;
            enemyPosY--;
            visionLineXMinus.UnionWith(visionLineXPlus);
            enemyVision.UnionWith(visionLineXMinus);
        } 
        return enemyVision;
    }
    public void EnemyCombatInitiate(HashSet<Vector2Int> enemyVision) 
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2Int playerPositionV2Int = new Vector2Int((int)playerPosition.x, (int)playerPosition.y);
        if (enemyVision.Contains(playerPositionV2Int)) { Debug.Log("Player in enemy sight!"); }
        else { Debug.Log("----------------------------"); }
    }
    public override void OnTick()
    {
        timer += Time.deltaTime;
        if (timer >= updateTimer)
        {   EnemyCombatInitiate(FoWVisionTiles(enemyStats));
            timer = 0; 
        }
    }
    public HashSet<Vector2Int> BresenhamsAlgorithm(Vector2Int start, Vector2Int end) 
    { 
        HashSet<Vector2Int> visionLine = new HashSet<Vector2Int>();
        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);
        int sx = (start.x < end.x)? 1: -1;
        int sy = (start.y < end.y)? 1: -1;
        int err = dx-dy;
        Vector3 wallBlockSight = GameObject.Find("Walls").GetComponent<Tilemap>().transform.position;
        Vector2Int wallPosition = new Vector2Int((int)wallBlockSight.x, (int)wallBlockSight.y);

        while (true)
        {
            if (start.x == end.x && start.y == end.y)
                break;
            if (visionLine.Contains(wallPosition)) 
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


//for (int x = -enemyStats.visionRadius; x <= enemyStats.visionRadius; x++)
//{
//    for (int y = -enemyStats.visionRadius; y <= enemyStats.visionRadius; y++)
//    {
//        float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
//        if (distanceFromCenter <= enemyStats.visionRadius)
//        {
//            //Vector2Int nextTilePosition = new Vector2Int(gridPosition.x + x, gridPosition.y + y);
//            //enemyVision.Add(nextTilePosition);
//            visionLineXPlus.UnionWith(visionLineXMinus);
//        }
//    }
//}