﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;

    public static float gSpeed = 3;
    public enum Event { RandomRocks, Octopus }

    public MoveDelete[] rocks;
    public GameObject octopus;

    private void Awake()
    {
        GM = this;

        InitEvent(Event.RandomRocks);
    }

    private void Update()
    {
            
    }

    public static void InitEvent(Event type)
    {
        switch (type)
        {
            case Event.RandomRocks:
                RandomRocks();
                break;
        }


    }

    static void RandomRocks()
    {

        for(int i = 0; i < 20; i++)
        {
            int x = Random.Range(10, 30);
            int y = Random.Range(-5, 1);

            Instantiate(GM.rocks[Random.Range(0, 2)], new Vector2(x, y), Quaternion.identity).speed = gSpeed;

        }


    }


}
