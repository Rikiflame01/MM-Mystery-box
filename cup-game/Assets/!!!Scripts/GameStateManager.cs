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
            DontDestroyOnLoad(gameObject);
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
                Debug.Log("State: Default");
                break;
            case GameState.Playing:
                OnPlayingState?.Invoke();
                Debug.Log("State: Playing");
                OnRollBall?.Invoke();
                break;
            case GameState.PlayerCorrect:
                OnPlayerCorrectState?.Invoke();
                Debug.Log("State: Player Correct");
                break;
            case GameState.PlayerIncorrect:
                OnPlayerIncorrectState?.Invoke();
                Debug.Log("State: Player Incorrect");
                break;
            case GameState.PlayerWon:
                OnPlayerWonState?.Invoke();
                Debug.Log("State: Player Won");
                break;
            case GameState.PlayerLost:
                OnPlayerLostState?.Invoke();
                Debug.Log("State: Player Lost");
                break;
            case GameState.PlayerSelecting:
                OnPlayerSelectingState?.Invoke();
                Debug.Log("State: Player Selecting");
                OnShuffleCups?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
