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
    public enum Event { RandomRocks, Octopus, GhostShip }

    public MoveDelete[] rocks;
    public MoveDelete octopus;
    public MoveDelete ghostShip;

    private void Awake()
    {
        GM = this;
        gSpeed = 3;
        dificulty = 1;
        InitEvent(Event.GhostShip);
    }

    private void Update()
    {
        if (interval < 0)
        {
            InitEvent((Event)Random.Range(0, 3));
            if (dificulty < maxDificulty) dificulty += .5f;
            else if (gSpeed < maxSpeed) gSpeed += .25f;

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
            case Event.GhostShip:
                GhostShip();
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

    static void GhostShip()
    {
        List<Vector2> positions = new List<Vector2>();
        int n = (int)(dificulty);

        while (n > 0)
        {
            int x = (Random.Range(0, 2) * 2 - 1);
            int y = Random.Range(-4, 1);

            bool canSpawn = true;

            for (int j = 0; j < positions.Count; j++)
            {
                if (positions[j].x == x * 10 && positions[j].y == y)
                {
                    canSpawn = false;
                }
            }

            if (canSpawn)
            {
                Instantiate(GM.ghostShip, new Vector2(x * 10, y), Quaternion.identity).speed = x * gSpeed;
                interval = 1 / gSpeed * 20;
                n--;
                positions.Add(new Vector2(x * 10, y));
            }
        }


    }

}
