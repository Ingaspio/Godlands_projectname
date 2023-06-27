using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerTurn : AbstractGameState
{
    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("Enter Combat Mode");
    }
    public override void UpdateState(GameManager gameManager)
    {
       if(Input.GetKeyDown("b"))
       {
        gameManager.SwitchState(gameManager.stateEnemyTurn);
       }
       if (Input.GetKeyDown("e"))
       {
        gameManager.SwitchState(gameManager.stateExploration);
       }
    }
}
