using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public float lerpDuration = 1f;
    private bool isLerping = false;
    private float defaultFOV = 47f;
    private float playingFOV = 30f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = playingFOV;
        StartCoroutine(LerpFOV(defaultFOV, GameState.Default));
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnDefaultState.AddListener(StartLerpToDefaultFOV);
            GameStateManager.Instance.OnPlayingState.AddListener(StartLerpToPlayingFOV);
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnDefaultState.RemoveListener(StartLerpToDefaultFOV);
            GameStateManager.Instance.OnPlayingState.RemoveListener(StartLerpToPlayingFOV);
        }
    }

    private void StartLerpToDefaultFOV()
    {
        if (!isLerping)
        {
            StartCoroutine(LerpFOV(defaultFOV, GameState.Default));
        }
    }

    private void StartLerpToPlayingFOV()
    {
        if (!isLerping)
        {
            StartCoroutine(LerpFOV(playingFOV, GameState.PlayerSelecting));
        }
    }

    private IEnumerator LerpFOV(float endValue, GameState nextState)
    {
        isLerping = true;
        float startValue = cam.fieldOfView;
        float timeElapsed = 0f;

        while (timeElapsed < lerpDuration)
        {
            cam.fieldOfView = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = endValue;
        isLerping = false;

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(nextState);
        }
    }
}
