
using System;

public enum GameState { WorldMap, Exploration, StartCombat, PlayerTurn, 
    PlayerMove, PlayerAction, EnemyTurn, EndCombat, FinGame, MenuWindow}
public class GameManager : MonoCache
{
    public static GameManager Instance { get; private set; }

    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateGameState(GameState newState) 
    {
        State = newState;
        switch (newState)
        {
            case GameState.WorldMap:
                break;
            case GameState.Exploration:
                break;
            case GameState.StartCombat:

                break;
            case GameState.PlayerTurn:
                break;
            case GameState.PlayerMove:
                break;
            case GameState.PlayerAction:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.EndCombat:
                break;
            case GameState.FinGame:
                break;
            case GameState.MenuWindow:
                break;
            default:
               newState = GameState.Exploration;
               break;

        }

        OnGameStateChanged?.Invoke(newState);
    }
}