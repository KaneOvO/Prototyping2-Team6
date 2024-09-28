using UnityEngine;
using System.Collections;
using Roger;

public class TreeGrowth : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 targetScale;
    public float growthScaleFactor = 3.0f;
    public float growthDuration = 30.0f;
    Roger.Tree tree;

    private void Start()
    {
        tree = GetComponent<Roger.Tree>();
        initialScale = transform.localScale;
        targetScale = initialScale * growthScaleFactor;

        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        float elapsedTime = 0f;

        while (elapsedTime < growthDuration)
        {
            if (!tree.isOnFire)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / growthDuration;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            }

            yield return null;
        }

        transform.localScale = targetScale;
    }
}