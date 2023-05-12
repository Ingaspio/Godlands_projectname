using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatInteractionStart
{
    public static void EnemyCombatInitiate(HashSet<Vector2Int> enemyVision)
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2Int playerPositionV2Int = new Vector2Int((int)playerPosition.x, (int)playerPosition.y);
        if (enemyVision.Contains(playerPositionV2Int)) { Debug.Log("Player in enemy sight!"); }
        else { Debug.Log("----------------------------"); }
    }
    public static void PlayerCombatInitiate() 
    { 
    
    }
}
