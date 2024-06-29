using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupShuffler : MonoBehaviour
{
    public Transform[] cups;
    public float shuffleDuration = 2f;
    public int shuffleCount = 3;

    public void StartShuffling()
    {
        StartCoroutine(ShuffleCups());
    }

    private IEnumerator ShuffleCups()
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            Transform cup1 = cups[Random.Range(0, cups.Length)];
            Transform cup2 = cups[Random.Range(0, cups.Length)];

            Vector3 cup1Position = cup1.position;
            Vector3 cup2Position = cup2.position;

            float timeElapsed = 0f;

            while (timeElapsed < shuffleDuration)
            {
                cup1.position = Vector3.Lerp(cup1Position, cup2Position, timeElapsed / shuffleDuration);
                cup2.position = Vector3.Lerp(cup2Position, cup1Position, timeElapsed / shuffleDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            cup1.position = cup2Position;
            cup2.position = cup1Position;
        }
    }
}
