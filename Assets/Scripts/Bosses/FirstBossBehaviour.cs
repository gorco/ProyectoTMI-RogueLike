using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBehaviour : MonoBehaviour {

    IAStates boss;
    Rigidbody2D my_rg;
    GameObject player;

	public float timeAttack;
	SweepingAttack weapon;

    // Use this for initialization
	void Start () {
		boss = new FirstBoss();
		my_rg = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
		weapon = GetComponentInChildren<SweepingAttack>();
		weapon.timeAttack = timeAttack;
		weapon.player = player;
	}
	
	// Update is called once per frame
	void Update () {
		

	}



}
