using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMove : MonoBehaviour
{
    public Transform other;
    public int xDist = 20;
    public float speedMod = 1f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -20) transform.position = other.position + new Vector3(xDist, 0, 0);
        transform.Translate(Vector3.left * GameManager.gSpeed * speedMod * Time.deltaTime);
    }
}
