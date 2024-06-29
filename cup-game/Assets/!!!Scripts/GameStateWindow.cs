using UnityEditor;
using UnityEngine;

public class GameStateWindow : EditorWindow
{
    [MenuItem("Window/Game State Manager")]
    public static void ShowWindow()
    {
        GetWindow<GameStateWindow>("Game State Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Game State Manager", EditorStyles.boldLabel);

        if (GUILayout.Button("Default State"))
        {
            ChangeGameState(GameState.Default);
        }

        if (GUILayout.Button("Playing State"))
        {
            ChangeGameState(GameState.Playing);
        }

        if (GUILayout.Button("Player Correct State"))
        {
            ChangeGameState(GameState.PlayerCorrect);
        }

        if (GUILayout.Button("Player Incorrect State"))
        {
            ChangeGameState(GameState.PlayerIncorrect);
        }

        if (GUILayout.Button("Player Won State"))
        {
            ChangeGameState(GameState.PlayerWon);
        }

        if (GUILayout.Button("Player Lost State"))
        {
            ChangeGameState(GameState.PlayerLost);
        }
    }

    private void ChangeGameState(GameState newState)
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(newState);
        }
        else
        {
            Debug.LogWarning("GameStateManager instance not found in the scene.");
        }
    }
}
