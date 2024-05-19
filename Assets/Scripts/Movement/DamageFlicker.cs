using System.Collections;
using UnityEngine;

public class DamageFlicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float flickerDuration = 0.2f;  // Durata totală a animației de flicker
    public float flickerInterval = 0.05f; // Intervalul de timp între flicker-uri

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartFlicker()
    {
        StartCoroutine(FlickerCoroutine());
    }

    private IEnumerator FlickerCoroutine()
    {
        float endTime = Time.time + flickerDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flickerInterval);
        }
        spriteRenderer.enabled = true; // Asigură-te că sprite-ul este vizibil la final
    }
}