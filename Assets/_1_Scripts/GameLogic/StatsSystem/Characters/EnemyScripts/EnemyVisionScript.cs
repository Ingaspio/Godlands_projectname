using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionScript : MonoCache
{
    public CharacterStatsSO enemyStats;
    private float timer = 0f;
    private float updateTimer = 3f;

    public HashSet<Vector2Int> EnemyVisionTiles(CharacterStatsSO enemyStats)
    {
        BresenhamAlgorythm bresenhamAlgorythm = new(); 
        Vector3 enemyPosition = this.transform.position;
        HashSet<Vector2Int> enemyVision = new HashSet<Vector2Int>();
        for (int i = 0; i < GetEndPoints().Count; i++)
        {
            Vector2Int enemyPosVector2Int = new Vector2Int((int)enemyPosition.x, (int)enemyPosition.y);
            HashSet<Vector2Int> visionLine = bresenhamAlgorythm.BresenhamsLineAlgorythm(enemyPosVector2Int, GetEndPoints()[i]);
            enemyVision.Add(enemyPosVector2Int);
            enemyVision.UnionWith(visionLine);
        }
        return enemyVision;
    }

    public List<Vector2Int> GetEndPoints()
    {
        Vector3 enemyPosition = this.transform.position;
        List<Vector2Int> endPoints = new();

        for (int x = -enemyStats.visionRadius; x <= enemyStats.visionRadius; x++)
        {
            for (int y = -enemyStats.visionRadius; y <= enemyStats.visionRadius; y++)
            {
                if (x * x + y * y <= enemyStats.visionRadius * enemyStats.visionRadius)
                {
                    endPoints.Add(new Vector2Int(x + (int)Mathf.Floor(enemyPosition.x), y + (int)Mathf.Floor(enemyPosition.y)));
                }
            }
        }
        return endPoints;
    }

    public override void OnTick()
    {
        // timer += Time.deltaTime;
        // if (timer >= updateTimer)
        // {   
        //     CombatInteractionStart.EnemyCombatInitiate(EnemyVisionTiles(enemyStats));
        //     timer = 0; 
        // }
    }
   
}
