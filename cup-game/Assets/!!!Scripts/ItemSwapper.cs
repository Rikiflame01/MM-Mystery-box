using UnityEngine;

public class ItemSwapper : MonoBehaviour
{
    public Transform[] items;

    private void OnEnable()
    {
        EventManager.StartListeningSwapItem0With1(() => SwapItems(0, 1));
        EventManager.StartListeningSwapItem1With2(() => SwapItems(1, 2));
        EventManager.StartListeningSwapItem2With0(() => SwapItems(2, 0));
        EventManager.StartListeningSwapItem0With4(() => SwapItems(0, 4));
    }

    private void OnDisable()
    {
        EventManager.StopListening("SwapItem0With1", () => SwapItems(0, 1));
        EventManager.StopListening("SwapItem1With2", () => SwapItems(1, 2));
        EventManager.StopListening("SwapItem2With0", () => SwapItems(2, 0));
        EventManager.StopListening("SwapItem0With4", () => SwapItems(0, 4));
    }

    public void SwapItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= items.Length || indexB < 0 || indexB >= items.Length)
        {
            Debug.LogError("Invalid indices for swapping.");
            return;
        }

        Transform itemA = items[indexA];
        Transform itemB = items[indexB];

        // Disable renderers
        if (indexA != 0 && indexA != 3) DisableRenderers(itemA);
        if (indexB != 0 && indexB != 3) DisableRenderers(itemB);

        if (indexA == 0 || indexA == 3) DisableMeshRenderer(itemA);
        if (indexB == 0 || indexB == 3) DisableMeshRenderer(itemB);

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

        // Enable renderers
        if (indexA != 0 && indexA != 3) EnableRenderers(itemA);
        if (indexB != 0 && indexB != 3) EnableRenderers(itemB);

        if (indexA == 0 || indexA == 3) EnableMeshRenderer(itemA);
        if (indexB == 0 || indexB == 3) EnableMeshRenderer(itemB);
    }

    private void DisableRenderers(Transform item)
    {
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void EnableRenderers(Transform item)
    {
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    private void DisableMeshRenderer(Transform item)
    {
        var meshRenderer = item.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    private void EnableMeshRenderer(Transform item)
    {
        var meshRenderer = item.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }
    }
}
