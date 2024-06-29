using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Transform ballChecker;

    private void OnMouseDown()
    {
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.currentState == GameState.Default)
            {
                GameStateManager.Instance.ChangeState(GameState.Playing);
            }
            else if (GameStateManager.Instance.currentState == GameState.PlayerSelecting)
            {
                HandleSelection();
            }
        }
    }

    private void HandleSelection()
    {
        bool hasBall = CheckForBall();

        if (hasBall)
        {
            Debug.Log("Player has selected the correct cup: " + gameObject.name);
            GameStateManager.Instance.ChangeState(GameState.PlayerCorrect);
        }
        else
        {
            Debug.Log("Player has selected the wrong cup: " + gameObject.name);
            GameStateManager.Instance.ChangeState(GameState.PlayerIncorrect);
        }
    }

    private bool CheckForBall()
    {
        return ballChecker.childCount > 0;
    }
}
