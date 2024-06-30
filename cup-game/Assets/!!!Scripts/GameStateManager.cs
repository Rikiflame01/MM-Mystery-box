using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Default,
    Playing,
    PlayerCorrect,
    PlayerIncorrect,
    PlayerWon,
    PlayerLost,
    PlayerSelecting
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState currentState;

    public UnityEvent OnDefaultState;
    public UnityEvent OnPlayingState;
    public UnityEvent OnPlayerCorrectState;
    public UnityEvent OnPlayerIncorrectState;
    public UnityEvent OnPlayerWonState;
    public UnityEvent OnPlayerLostState;
    public UnityEvent OnPlayerSelectingState;
    public UnityEvent OnRollBall;
    public UnityEvent OnShuffleCups;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Default);
    }

    public void ChangeState(GameState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        HandleStateChange(newState);
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Default:
                OnDefaultState?.Invoke();
                break;
            case GameState.Playing:
                OnPlayingState?.Invoke();
                OnRollBall?.Invoke();
                break;
            case GameState.PlayerCorrect:
                OnPlayerCorrectState?.Invoke();
                break;
            case GameState.PlayerIncorrect:
                OnPlayerIncorrectState?.Invoke();
                break;
            case GameState.PlayerWon:
                OnPlayerWonState?.Invoke();
                break;
            case GameState.PlayerLost:
                OnPlayerLostState?.Invoke();
                break;
            case GameState.PlayerSelecting:
                OnPlayerSelectingState?.Invoke();
                OnShuffleCups?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
