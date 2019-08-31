using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSprite : MonoBehaviour
{
    SpriteRenderer srender;

    Vector2 originalScale;
    Color originalColor;

    private void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
        originalColor = srender.color;
        originalScale = transform.localScale;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            StopAllCoroutines();
            srender.enabled = true;
            transform.localScale = originalScale;
            srender.color = originalColor;
        }
        else
        {
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

}
