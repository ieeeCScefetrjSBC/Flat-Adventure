﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMove : MonoBehaviour
{
    public Transform other;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -20) transform.position = other.position + new Vector3(20, 0, 0);
        transform.Translate(Vector3.left * GameManager.gSpeed * Time.deltaTime);
    }
}
