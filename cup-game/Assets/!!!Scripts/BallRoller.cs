using System.Collections;
using UnityEngine;

public class BallRoller : MonoBehaviour
{
    public Transform ball;
    public Transform targetCup; 
    public Transform targetEmpty; 
    public float rollDuration = 1f;
    public float rotateDuration = 0.5f;
    public float liftHeight = 0.5f; 

    public void StartRolling()
    {
        StartCoroutine(RollBall());
    }

    private IEnumerator RollBall()
    {
        Vector3 startPosition = ball.position;
        Vector3 cupStartPosition = targetCup.position;
        Vector3 cupLiftedPosition = cupStartPosition + Vector3.up * liftHeight;
        Vector3 ballEndPosition = targetEmpty.position;
        Quaternion cupStartRotation = targetCup.rotation;
        Quaternion cupOpenRotation = Quaternion.Euler(cupStartRotation.eulerAngles.x - 90f, cupStartRotation.eulerAngles.y, cupStartRotation.eulerAngles.z);

        float timeElapsed = 0f;
        while (timeElapsed < rotateDuration)
        {
            targetCup.position = Vector3.Lerp(cupStartPosition, cupLiftedPosition, timeElapsed / rotateDuration);
            targetCup.rotation = Quaternion.Lerp(cupStartRotation, cupOpenRotation, timeElapsed / rotateDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        targetCup.position = cupLiftedPosition;
        targetCup.rotation = cupOpenRotation;

        timeElapsed = 0f;
        while (timeElapsed < rollDuration)
        {
            ball.position = Vector3.Lerp(startPosition, ballEndPosition, timeElapsed / rollDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ball.position = ballEndPosition;
        ball.SetParent(targetEmpty);

        timeElapsed = 0f;
        while (timeElapsed < rotateDuration)
        {
            targetCup.position = Vector3.Lerp(cupLiftedPosition, cupStartPosition, timeElapsed / rotateDuration);
            targetCup.rotation = Quaternion.Lerp(cupOpenRotation, cupStartRotation, timeElapsed / rotateDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        targetCup.position = cupStartPosition;
        targetCup.rotation = cupStartRotation;

        GameStateManager.Instance.ChangeState(GameState.PlayerSelecting);
    }
}
