using System.Collections;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public bool highlightEnabled = false;
    public float lerpDuration = 1f;
    private Material material;
    private Color originalColor;
    private bool isHighlighting = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    void Update()
    {
        if (highlightEnabled && !isHighlighting)
        {
            StartCoroutine(LerpColorToWhite());
        }
        else if (!highlightEnabled && isHighlighting)
        {
            StartCoroutine(LerpColorToOriginal());
        }
    }

    private IEnumerator LerpColorToWhite()
    {
        isHighlighting = true;
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            material.color = Color.Lerp(originalColor, Color.white, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        material.color = Color.white;
        yield return new WaitForSeconds(lerpDuration);
        StartCoroutine(LerpColorToOriginal());
    }

    private IEnumerator LerpColorToOriginal()
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            material.color = Color.Lerp(Color.white, originalColor, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        material.color = originalColor;
        isHighlighting = false;
    }
}
