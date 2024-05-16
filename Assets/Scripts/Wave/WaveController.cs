using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextController : MonoBehaviour
{
    public Text waveText;
    public float fadeInDuration = 1f;
    public float displayDuration = 2f;
    public float fadeOutDuration = 1f;

    private void Start()
    {
        // Asigură-te că textul este complet transparent la început
        Color color = waveText.color;
        color.a = 0;
        waveText.color = color;
    }

    public void DisplayWaveText(int waveNumber)
    {
        waveText.text = "Wave " + waveNumber;
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade In
        yield return StartCoroutine(FadeTextToFullAlpha(fadeInDuration, waveText));
        // Wait for display duration
        yield return new WaitForSeconds(displayDuration);
        // Fade Out
        yield return StartCoroutine(FadeTextToZeroAlpha(fadeOutDuration, waveText));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
