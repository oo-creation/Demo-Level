﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatConstructor : MonoBehaviour
{
    public GameObject Boat;
    public GameObject Paddle1;
    public GameObject Paddle2;

    public bool BoatComplete;

    public void ConstructStep()
    {
        if (!Boat.activeSelf)
        {
            Boat.SetActive(true);
        }
        else if (!Paddle1.activeSelf)
        {
            Paddle1.SetActive(true);
        }
        else if (!Paddle2.activeSelf)
        {
            Paddle2.SetActive(true);
        }
        else
        {
            BoatComplete = true;
        }
    }
}