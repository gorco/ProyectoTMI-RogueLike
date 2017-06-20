using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossBehavior : MonoBehaviour
{

	GameObject player;

	public float minDistance = 3;

	private EnemyMovement movement;
	private LifeEnemy life;
	private EnemyAttack melee;
	private DistanceAttackEnemy distance;
    private Animator anim;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		life = GetComponentInChildren<LifeEnemy>();

		distance = GetComponent<DistanceAttackEnemy>();
		melee = GetComponentInChildren<EnemyAttack>();

		distance.recarga = 1.5f;
		melee.speedAttack = 2f;

        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		Debug.LogWarning(Vector3.Distance(transform.position, player.transform.position));
		if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
		{
            anim.SetTrigger("attack");
			melee.enabled = true;
			distance.enabled = false;
		}
		else
		{
            anim.SetTrigger("distancia");
            distance.enabled = true;
			melee.enabled = false;
		}

		if (life.GetCurrentLife() < life.maxLife / 4)
		{
			distance.recarga = 0.75f;
			melee.speedAttack = 1f;
		}
	}
}

