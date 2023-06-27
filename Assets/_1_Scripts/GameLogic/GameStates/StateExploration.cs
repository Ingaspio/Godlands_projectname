using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateExploration : AbstractGameState
{
    public override void EnterState(GameManager gameManager)
    {
        Debug.Log("You are now in Exploration Mode");
    }
    public override void UpdateState(GameManager gameManager)
    {
         if (Input.GetKeyDown("b"))
        {
            gameManager.SwitchState(gameManager.statePlayerTurn);
        }
    }
}
