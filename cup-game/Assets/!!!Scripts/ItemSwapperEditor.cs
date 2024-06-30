using UnityEditor;
using UnityEngine;

public class ItemSwapperEditor : EditorWindow
{
    private ItemSwapper itemSwapper;
    private int indexA;
    private int indexB;

    [MenuItem("Window/Item Swapper")]
    public static void ShowWindow()
    {
        GetWindow<ItemSwapperEditor>("Item Swapper");
    }

    private void OnGUI()
    {
        GUILayout.Label("Item Swapper", EditorStyles.boldLabel);

        itemSwapper = (ItemSwapper)EditorGUILayout.ObjectField("Item Swapper Script", itemSwapper, typeof(ItemSwapper), true);

        indexA = EditorGUILayout.IntField("Index A", indexA);
        indexB = EditorGUILayout.IntField("Index B", indexB);

        if (GUILayout.Button("Swap Items"))
        {
            if (itemSwapper != null)
            {
                itemSwapper.SwapItems(indexA, indexB);
            }
            else
            {
                Debug.LogError("ItemSwapper script is not assigned.");
            }
        }
    }
}
