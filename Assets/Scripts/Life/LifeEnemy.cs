using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour {

    private float maxLife = 70;
    public float life = 70;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (life > maxLife)
            life = maxLife;
        if (life < 0)
            life = 0;
    }
}
