using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBehaviour : MonoBehaviour {

    GameObject player;	
	SweepingAttack weapon;

	public float minDistance = 3;
	public float timeAttack = 2;

	private EnemyMovement movement;
	private LifeEnemy life;

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		weapon = GetComponentInChildren<SweepingAttack>();
		movement = GetComponent<EnemyMovement>();
		life = GetComponentInChildren<LifeEnemy>();
		weapon.timeAttack = timeAttack;
		weapon.player = player;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) < minDistance)
		{
			movement.enabled = false;
		} else
		{
			movement.enabled = true;
		}

		if(life.GetCurrentLife() < life.maxLife / 4)
		{
			weapon.attackLc = 25;
			weapon.altAttackLc = 35;
		}

	}



}
