using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer srendender;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider2D col;

    bool chase = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        srendender = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Rise()
    {
        col.enabled = true;
        animator.SetTrigger("Rise");
        chase = true;
    }

    private void Update()
    {
        if (chase)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.P.transform.position, GameManager.gSpeed / 4 * Time.deltaTime);
        }
    }

    public void WTF(float time)
    {
        col.enabled = false;
        StartCoroutine(Vanishing());
        Destroy(gameObject, time);
    }

    IEnumerator Vanishing()
    {
        yield return new WaitForSeconds(.5f);
        float time = 1f;
        while (time > 0)
        {
            Color color = new Color(1, 1, 1,time);
            srendender.color = color;
            time -= Time.deltaTime;
            yield return null;
        }
    }

}

