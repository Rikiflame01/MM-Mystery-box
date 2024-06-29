using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupShuffler : MonoBehaviour
{
    public Transform[] cups;
    public Transform[] shufflePositions;
    public float shuffleDuration = 2f;
    public int shuffleCount = 3;
    public float shuffleSpeedMultiplier = 1f;
    public Transform ball; // Reference to the ball
    public Rigidbody ballRigidbody; // Reference to the ball's rigidbody

    private Rigidbody[] cupRigidbodies;
    private Quaternion[] initialRotations;
    private Transform currentBallTarget;
    private float positionTolerance = 0.01f;

    private void Start()
    {
        cupRigidbodies = new Rigidbody[cups.Length];
        for (int i = 0; i < cups.Length; i++)
        {
            cupRigidbodies[i] = cups[i].GetComponent<Rigidbody>();
        }
        initialRotations = new Quaternion[cups.Length];
        ResetCupsToInitialPositions();
        if (ball != null)
        {
            currentBallTarget = ball.parent;
        }
    }

    public void StartShuffling()
    {
        StartCoroutine(ShuffleCups());
    }

    private IEnumerator ShuffleCups()
    {
        DisableRigidbodies();
        LockBallPosition();

        for (int i = 0; i < shuffleCount; i++)
        {
            int[] permutation = GenerateRandomPermutation(shufflePositions.Length);

            List<Vector3> newPositions = new List<Vector3>();
            for (int j = 0; j < cups.Length; j++)
            {
                Vector3 startPosition = cups[j].position;
                Quaternion startRotation = cups[j].rotation;
                Vector3 endPosition = shufflePositions[permutation[j]].position;

                while (newPositions.Contains(endPosition))
                {
                    permutation = GenerateRandomPermutation(shufflePositions.Length);
                    endPosition = shufflePositions[permutation[j]].position;
                }

                newPositions.Add(endPosition);
                yield return StartCoroutine(MoveCup(cups[j], startPosition, startRotation, endPosition));
            }

            // Update ball position frequently
            UpdateBallPosition();
        }

        EnableRigidbodies();
        UnlockBallPosition();
        UpdateBallPosition();
    }

    private void DisableRigidbodies()
    {
        foreach (Rigidbody rb in cupRigidbodies)
        {
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = true;
        }
    }

    private void EnableRigidbodies()
    {
        foreach (Rigidbody rb in cupRigidbodies)
        {
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = false;
        }
    }

    private void LockBallPosition()
    {
        if (ball != null)
        {
            ballRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void UnlockBallPosition()
    {
        if (ball != null)
        {
            ballRigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    private void UpdateBallPosition()
    {
        if (ball != null && currentBallTarget != null)
        {
            ball.position = currentBallTarget.position;
        }
    }

    private int[] GenerateRandomPermutation(int length)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < length; i++)
        {
            indices.Add(i);
        }

        int[] permutation = new int[length];
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, indices.Count);
            permutation[i] = indices[randomIndex];
            indices.RemoveAt(randomIndex);
        }

        return permutation;
    }

    private IEnumerator MoveCup(Transform cup, Vector3 startPosition, Quaternion startRotation, Vector3 endPosition)
    {
        float adjustedDuration = shuffleDuration / shuffleSpeedMultiplier;
        float timeElapsed = 0f;
        while (timeElapsed < adjustedDuration)
        {
            cup.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / adjustedDuration);
            cup.rotation = startRotation;
            UpdateBallPosition(); // Update ball position frequently
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cup.position = endPosition;
        cup.rotation = startRotation;
    }

    public void ResetCupsToInitialPositions()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].position = shufflePositions[i].position;
            initialRotations[i] = cups[i].rotation;
        }
        UpdateBallPosition();
    }
}
