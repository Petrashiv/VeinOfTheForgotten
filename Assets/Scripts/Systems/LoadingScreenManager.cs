using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    private Coroutine fadeCoroutine;
    public CanvasGroup loadingScreenGroup;
    
    public float fadeDuration = 1f;
    public Image blinkingImage;
    public float blinkSpeed = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (loadingScreenGroup.alpha > 0 && blinkingImage != null)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;
            Color color = blinkingImage.color;
            color.a = alpha;
            blinkingImage.color = color;
        }
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(FadeScenes(sceneName));
    }

    public void ShowLoadingScreen()
    {
        StartCoroutine(FadeCanvas(loadingScreenGroup, 0, 1));
    }

    

    private IEnumerator FadeScenes(string sceneName)
    {
        // Появление старого загрузочного экрана
        yield return StartCoroutine(FadeCanvas(loadingScreenGroup, 0, 1));

        

        yield return new WaitForSeconds(0.5f);

        // Загрузка новой сцены
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
            yield return null;

        asyncLoad.allowSceneActivation = true;
        yield return null;

        // === НАЙТИ загрузочный экран В НОВОЙ СЦЕНЕ ===
        yield return null; // Подождать один кадр, чтобы всё инициализировалось

        

        

        
    }


    private IEnumerator FadeCanvas(CanvasGroup group, float from, float to)
    {
        float time = 0f;
        group.alpha = from;
        group.blocksRaycasts = false;

        while (time < fadeDuration)
        {
            group.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        group.alpha = to;
    }
}

