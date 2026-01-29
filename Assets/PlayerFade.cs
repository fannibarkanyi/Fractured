using System.Collections;
using UnityEngine;

public class PlayerFade : MonoBehaviour
{
    public float fadeTime = 0.3f;

    private SpriteRenderer[] renderers;

    void Awake()
    {
        RefreshRenderers();
    }

    public void RefreshRenderers()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public IEnumerator FadeOut()
    {
        RefreshRenderers();
        SetAlphaInstant(1f);
        yield return Fade(1f, 0f);
    }

    public IEnumerator FadeIn()
    {
        RefreshRenderers();
        SetAlphaInstant(0f);
        yield return Fade(0f, 1f);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeTime);
            SetAlphaInstant(a);
            yield return null;
        }

        SetAlphaInstant(to);
    }

    private void SetAlphaInstant(float a)
    {
        if (renderers == null) return;

        foreach (var r in renderers)
        {
            if (r == null) continue;
            Color c = r.color;
            c.a = a;
            r.color = c;
        }
    }
}
