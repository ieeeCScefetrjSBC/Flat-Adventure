using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player P = null;

    public int maxHp;
    public int hp;

    Rigidbody2D rb2d;
    bool invencible = false;
    SpriteRenderer srender;

    private void Awake()
    {
        hp = maxHp;
        P = this;
        rb2d = GetComponent<Rigidbody2D>();
        srender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();


    }

    void MoveUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((x < 0 && transform.position.x <= -8.5f) || (x > 0 && transform.position.x >= 8.5f)) x = 0;
        if ((y < 0 && transform.position.y <= -4.5f) || (y > 0 && transform.position.y >= 1f)) y = 0;

        rb2d.velocity = (GameManager.gSpeed + 2) * new Vector2(x, y).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invencible) return;
        StopAllCoroutines();
        StartCoroutine(Bounce());
        StartCoroutine(Blink());
        EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, 1, .1f);

        hp--;
        if (hp <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Bounce()
    {
        transform.localScale = Vector2.one * 2f;
        float time = .5f;
        while (time > 0)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 10f * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Blink()
    {
        invencible = true;

        yield return new WaitForSeconds(.25f);

        float time = 1.75f;
        bool lol = false;
        Color color = new Color(1, 1, 1, 0);
        while (time > 0)
        {
            srender.color = lol ? Color.white : color;
            lol = !lol;
            yield return new WaitForSeconds(.1f);
            time -= .1f;
        }

        srender.color = Color.white;

        invencible = false;
    }

}
