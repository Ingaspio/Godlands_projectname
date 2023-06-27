using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyTurn : AbstractGameState
{
    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Enemy Turn...");
    }
    public override void UpdateState(GameManager gameManager)
    {
        if(Input.GetKeyDown("b"))
       {
        gameManager.SwitchState(gameManager.statePlayerTurn);
       }
    }
}
