using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player P = null;

    public int maxHp;
    public int hp;
    public PlayerHpUI hpUI;
    public CannonBall cannonBall;
    public Camera cam;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D boatLight;
    public AmmoSprite[] ammunitionSprite;

    Rigidbody2D rb2d;
    bool invencible = false;
    SpriteRenderer srender;
    float shootInterval;
    int ammunition = 3;

    private void Awake()
    {
        ammunition = ammunitionSprite.Length;
        hp = maxHp;
        P = this;
        rb2d = GetComponent<Rigidbody2D>();
        srender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver) return;

        MoveUpdate();
        ShootUpdate();
    }

    void ShootUpdate()
    {
        if (shootInterval <= 0)
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.paused && ammunition > 0)
            {
                Vector3 dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                dir.z = 0;
                dir.Normalize();
                Instantiate(cannonBall, transform.position + dir * .5f, Quaternion.identity).SetVelocity(dir * 10f);
                shootInterval = .25f;
                ammunition--;
                UpdateAmunition();
            }
        }
        else
        {
            shootInterval -= Time.deltaTime;
        }


    }

    void MoveUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((x < 0 && transform.position.x <= -8.5f) || (x > 0 && transform.position.x >= 8.5f)) x = 0;
        if ((y < 0 && transform.position.y <= -4.5f) || (y > 0 && transform.position.y >= 1f)) y = 0;

        rb2d.velocity = (GameManager.gSpeed + 2) * new Vector2(x, y).normalized;
    }

    public void UpdateAmunition()
    {
        for (int i = 0; i < ammunition; i++) ammunitionSprite[i].SetState(true);
        for (int i = ammunition; i < ammunitionSprite.Length; i++) ammunitionSprite[i].SetState(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(transform.tag)) return;

        if (collision.CompareTag("Barrel"))
        {
            ammunition = ammunitionSprite.Length;
            UpdateAmunition();
            return;
        }
        else if (invencible) return;

        StopAllCoroutines();
        StartCoroutine(Bounce());
        StartCoroutine(Blink());
        EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, 1, .1f);

        hp--;
        hpUI.SetFill(hp, maxHp);
        if (hp <= 0)
        {
            GameManager.GameOver();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            rb2d.velocity = Vector2.zero;
            boatLight.enabled = false;
            for (int i = 0; i < ammunitionSprite.Length; i++) ammunitionSprite[i].srender.enabled = false;
        }
    }

    IEnumerator Bounce()
    {
        hpUI.transform.localScale = transform.localScale = Vector2.one * 2f;
        float time = .5f;
        while (time > 0)
        {
            hpUI.transform.localScale = transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 10f * Time.deltaTime);
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
