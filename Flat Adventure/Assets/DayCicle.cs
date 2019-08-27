using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCicle : MonoBehaviour
{
    public Transform sun;
    public Transform moon;
    public Vector2 radius;
    public float speed;


    float angle = 0;

    private void Update()
    {

        float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x;
        float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius.y;

        sun.localPosition = new Vector2(x, y);
        moon.localPosition = new Vector2(-x, -y);

        angle += speed * Time.deltaTime;

    }

}
