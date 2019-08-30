using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDelete : MonoBehaviour
{
    [HideInInspector]public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
