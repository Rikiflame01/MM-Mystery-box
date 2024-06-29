using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(GameState.Default);
        }
    }
}
