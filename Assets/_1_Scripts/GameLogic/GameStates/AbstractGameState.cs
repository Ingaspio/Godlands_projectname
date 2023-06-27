using UnityEngine;

public abstract class AbstractGameState
{
    public abstract void EnterState(GameManager gameManager);
    public abstract void UpdateState(GameManager gameManager);
}
