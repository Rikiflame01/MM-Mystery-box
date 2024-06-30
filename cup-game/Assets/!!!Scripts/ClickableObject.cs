using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Transform ballChecker;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.currentState == GameState.PlayerSelecting)
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
            gameManager.SetSelection(true);
        }
        else
        {
            gameManager.SetSelection(false);
        }
    }

    private bool CheckForBall()
    {
        return ballChecker.childCount > 0;
    }
}
