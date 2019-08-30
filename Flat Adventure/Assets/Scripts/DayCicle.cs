using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCicle : MonoBehaviour
{
    public static float y;

    public Transform sun;
    public Transform moon;
    public Vector2 radius;
    public float speed;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D boatLight;
    public Color dayColor;
    public Color nightColor;

    float angle = 0;

    private void Update()
    {

        float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x;
        y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius.y;

        sun.localPosition = new Vector2(x, y);
        moon.localPosition = new Vector2(-x, -y);

        angle += speed * Time.deltaTime;

        y = (y / radius.y + 1) / 2f;

        globalLight.color = y * dayColor + (1f - y) * nightColor;
        boatLight.intensity = 1f - y;
    }




}
