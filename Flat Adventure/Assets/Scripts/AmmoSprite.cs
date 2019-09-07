using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSprite : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer srender;

    Vector2 originalScale;
    Color originalColor;

    private void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
        originalColor = srender.color;
        originalScale = transform.localScale;
    }

    bool state = true;

    public void SetState(bool s)
    {
        if (s == state) return;
        state = s;

        if (state)
        {
            StopAllCoroutines();
            transform.localScale = Vector2.one;
            srender.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            StartCoroutine(UNFading());
        }
        else
        {
            StopAllCoroutines();
            transform.localScale = originalScale;
            srender.color = originalColor;
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        float time = .5f;
        while (time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, Time.deltaTime);
            srender.color = new Color(originalColor.r, originalColor.g, originalColor.b, 2f * time);
            time -= Time.deltaTime;
            yield return null;
        }

        srender.enabled = false;
    }

    IEnumerator UNFading()
    {
        srender.enabled = true;

        float time = .5f;
        while (time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, originalScale, 5f * Time.deltaTime);
            srender.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - 2f * time);
            time -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        srender.color = originalColor;

    }

}
