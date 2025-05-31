using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public Image blinkingImage;
    public float blinkSpeed = 2f;
    private Coroutine fadeCoroutine;
    private void Start()
    {
        FadeOut();
    }
    private void Update()
    {
        if (canvasGroup.alpha > 0 && blinkingImage != null)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;
            Color color = blinkingImage.color;
            color.a = alpha;
            blinkingImage.color = color;
        }
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f, disableRaycastAfter: true));
    }

    private IEnumerator FadeTo(float targetAlpha, bool disableRaycastAfter = false)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (disableRaycastAfter)
            canvasGroup.blocksRaycasts = false;
    }
    public void NewGameButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    public void ContinueButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    
    public void ExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
