using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Transform ballChecker; // Reference to the empty object that checks for the ball
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
                // Handle the player's selection
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
            gameManager.SetSelection(true);
        }
        else
        {
            Debug.Log("Player has selected the wrong cup: " + gameObject.name);
            gameManager.SetSelection(false);
        }
    }

    private bool CheckForBall()
    {
        return ballChecker.childCount > 0;
    }
}
