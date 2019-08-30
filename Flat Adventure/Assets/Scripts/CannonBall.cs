using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    Rigidbody2D rb2d;
    SpriteRenderer ren;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 10f);
    }

    public void SetVelocity(Vector2 dir)
    {
        StartCoroutine(Growing());
        rb2d.velocity = dir;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GhostShip") || collision.CompareTag("GhostBall")) return;
        Destroy(gameObject);
    }

    IEnumerator Growing()
    {
        float time = 1f;
        while(time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * .5f, 20f * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
