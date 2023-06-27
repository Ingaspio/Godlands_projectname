
using System;

// public enum AbstractGameState { WorldMap, Exploration, PlayerTurn, 
//      EnemyTurn, FinGame, MenuWindow}
public class GameManager : MonoCache
{
    public static GameManager Instance { get; private set; }

    public AbstractGameState currentState;
    public StateEnemyTurn stateEnemyTurn = new StateEnemyTurn();
    public StatePlayerTurn  statePlayerTurn = new StatePlayerTurn();
    public StateExploration stateExploration = new StateExploration();
    public static event Action<AbstractGameState> OnGameStateChanged;
    private void Start()
    {
        Instance = this;
        currentState = stateExploration;
        currentState.EnterState(this);
    }
    private void Update() {
        currentState.UpdateState(this);
    }

    public void SwitchState(AbstractGameState gameState)
    {
        currentState = gameState;
        gameState.EnterState(this);
    } 
   
}