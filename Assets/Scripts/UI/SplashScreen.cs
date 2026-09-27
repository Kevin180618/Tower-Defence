using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Text percentText;

    [Header("Scene")]
    [SerializeField] private string sceneToLoad;

    [Header("Settings")]
    [SerializeField] private float firstPhaseTime = 3f;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        operation.allowSceneActivation = false;

        float timer = 0f;

        while (timer < firstPhaseTime)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / firstPhaseTime);

            // 0 -> 0.5
            float progress = Mathf.Lerp(0f, 0.9f, t);

            UpdateProgress(progress);

            yield return null;
        }

        UpdateProgress(0.9f);

        while (operation.progress < 0.9f)
        {
            float asyncProgress = operation.progress / 0.9f;

            float progress = Mathf.Lerp(0.9f, 1f, asyncProgress);

            UpdateProgress(progress);

            yield return null;
        }

        UpdateProgress(1f);

        operation.allowSceneActivation = true;
    }

    private void UpdateProgress(float progress)
    {
        loadingSlider.value = progress;

        int percentage = Mathf.RoundToInt(progress * 100f);

        percentText.text = percentage + "%";
    }
}