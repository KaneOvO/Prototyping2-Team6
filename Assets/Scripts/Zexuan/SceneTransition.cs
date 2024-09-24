using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;
    public bool isMainMenu = false;

    private void Start()
    {
        if (!isMainMenu)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        UIManager.Instance.isTransitioning = true;
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        UIManager.Instance.fadeImage.SetActive(false);
        UIManager.Instance.isTransitioning = false;
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        UIManager.Instance.isTransitioning = true;
        UIManager.Instance.fadeImage.SetActive(true);
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        UIManager.Instance.isTransitioning = false;
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}