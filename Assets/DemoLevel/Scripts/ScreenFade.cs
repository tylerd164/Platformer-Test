using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image screenFadeImage;
    [SerializeField] private Image screenFadeImageRespawn;
    [SerializeField] private GameObject screenFadeObjectMiniGame;
    [SerializeField] private GameObject screenFadeObjectRespawn;

    public float fadeDuration = 1f;
    public float fullImageTime = 1f;

    public IEnumerator FadeOut()
    {
        screenFadeObjectMiniGame.SetActive(true);

        float t = 0f;
        Color color = screenFadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(fullImageTime, 0f, t / fadeDuration);
            screenFadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        screenFadeImage.color = color;
        screenFadeObjectMiniGame.SetActive(false);
    }

    public IEnumerator FadeOutRespawn()
    {
        screenFadeObjectRespawn.SetActive(true);

        float t = 0f;
        Color color = screenFadeImageRespawn.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(fullImageTime, 0f, t / fadeDuration);
            screenFadeImageRespawn.color = color;
            yield return null;
        }

        color.a = 0f;
        screenFadeImageRespawn.color = color;
        screenFadeObjectRespawn.SetActive(false);
    }
}