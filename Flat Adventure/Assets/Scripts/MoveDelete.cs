using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDelete : MonoBehaviour
{
    float speed = 3;

    public void SetSpeed(float s, bool flip = false)
    {
        speed = s;
        if (flip) GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((speed > 0 && transform.position.x <= -10 )|| (speed < 0 && transform.position.x >= 10))
        {
            //Debug.Log(speed + "|" + transform.position.x);
            Destroy(gameObject);
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
