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
    
    IEnumerator Growing()
    {
        Vector2 dest = transform.localScale;
        transform.localScale = Vector2.zero;

        float time = 1f;
        while(time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, dest, 10f * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
