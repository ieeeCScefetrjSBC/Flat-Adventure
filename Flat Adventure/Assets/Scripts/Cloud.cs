using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    SpriteRenderer srender;

    private void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        srender.color = new Color(1, 1, 1, DayCicle.y);
    }

}
