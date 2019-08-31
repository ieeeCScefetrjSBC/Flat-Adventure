using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BounceDelete : MonoBehaviour
{
    bool trigged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigged || collision.CompareTag(transform.tag)) return;

        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(Triggering());
    }

    IEnumerator Triggering()
    {
        transform.localScale = Vector2.one * 2.5f;
        float time = .25f;
        while (time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 10f * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }

}
