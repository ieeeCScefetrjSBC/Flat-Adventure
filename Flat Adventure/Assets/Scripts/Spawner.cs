using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MoveDelete toSpawn;

    // Update is called once per frame
    private void Awake()
    {
        interval = Random.Range(0f, 2f);
    }
    float interval;

    void Update()
    {
        if (interval <= 0)
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity).SetSpeed(Random.Range(2f, 5f));
            interval = Random.Range(2f, 4f);
        }
        else
        {
            interval -= Time.deltaTime;
        }
    }
}
