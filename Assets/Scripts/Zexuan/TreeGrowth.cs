using UnityEngine;
using System.Collections;

public class TreeGrowth : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 targetScale ;
    public float growthScaleFactor = 3.0f;
    public float growthDuration = 30.0f;

    private void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale * growthScaleFactor;

        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        float elapsedTime = 0f;

        while (elapsedTime < growthDuration)
        {
            float progress = elapsedTime / growthDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}