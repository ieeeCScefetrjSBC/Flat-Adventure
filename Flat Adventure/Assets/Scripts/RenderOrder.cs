using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer srender;

    void Awake()
    {
        srender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        srender.sortingOrder = -(int)(10 * transform.position.y);
    }
}
