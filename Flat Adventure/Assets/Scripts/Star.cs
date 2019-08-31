using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.LWRP.Light2D light;
    SpriteRenderer srender;

    private void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        srender.color = new Color(1, 1, 1, 1f - DayCicle.y);
        light.intensity = 1f - DayCicle.y;
    }

}
