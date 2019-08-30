using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;

    public static float maxSpeed = 6;
    public static float maxDificulty = 5;
    public static float gSpeed = 3;
    public static float dificulty = 3;
    public static float interval = 0;
    public enum Event { RandomRocks, Octopus }

    public MoveDelete[] rocks;
    public MoveDelete octopus;

    private void Awake()
    {
        GM = this;

        InitEvent(Event.Octopus);
    }

    private void Update()
    {
        if (interval < 0)
        {
            InitEvent((Event)Random.Range(0, 2));
            if (gSpeed < maxSpeed) gSpeed += .5f;
            else if (dificulty < maxDificulty) dificulty += .5f;

        }
        else
        {
            interval -= Time.deltaTime;
        }
    }

    public static void InitEvent(Event type)
    {
        switch (type)
        {
            case Event.RandomRocks:
                RandomRocks();
                break;
            case Event.Octopus:
                Octopus();
                break;
        }
    }

    static void RandomRocks()
    {
        for (int i = 0; i < (int)(dificulty * 5); i++)
        {
            int x = Random.Range(10, 30);
            int y = Random.Range(-5, 2);

            Instantiate(GM.rocks[Random.Range(0, 2)], new Vector2(x, y), Quaternion.identity).speed = gSpeed;

        }

        interval = 1 / gSpeed * 30;
    }

    static void Octopus()
    {

        int y = Random.Range(-4, 1);
        Instantiate(GM.octopus, new Vector2(10, y), Quaternion.identity).speed = gSpeed;
        interval = 1 / gSpeed * 20;
    }

}
