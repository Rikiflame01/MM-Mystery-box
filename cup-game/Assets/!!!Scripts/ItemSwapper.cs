using UnityEngine;

public class ItemSwapper : MonoBehaviour
{
    public Transform[] items;

    public void SwapItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= items.Length || indexB < 0 || indexB >= items.Length)
        {
            Debug.LogError("Invalid indices for swapping.");
            return;
        }

        Transform itemA = items[indexA];
        Transform itemB = items[indexB];

        Transform parentA = itemA.parent;
        Transform parentB = itemB.parent;

        Vector3 positionA = itemA.position;
        Vector3 positionB = itemB.position;

        Quaternion rotationA = itemA.rotation;
        Quaternion rotationB = itemB.rotation;

        // Detach items from their parents
        itemA.parent = null;
        itemB.parent = null;

        // Swap positions
        itemA.position = positionB;
        itemB.position = positionA;

        // Restore original rotations
        itemA.rotation = rotationA;
        itemB.rotation = rotationB;

        // Reattach items to their original parents
        itemA.parent = parentB;
        itemB.parent = parentA;
    }
}
