﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHero : MonoBehaviour {

    private float maxLife = 100;
    public float life = 100;
    public float defensa = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life > maxLife)
            life = maxLife;
        if (life < 0)
            life = 0;
    }
}
