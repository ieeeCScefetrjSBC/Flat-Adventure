using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

        rb2d.velocity = speed * new Vector2(x, y).normalized;
    }



}
