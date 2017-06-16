using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingAttack : MonoBehaviour {

	private Quaternion targetRotation;
	public GameObject player;

	public bool doAltAttack = false;
	public bool doAttack = true;

	private float timeRotating = 0;

	public float timeAttack = 2;
	public float waitBAttack = 0;

	private Vector3 originalAngle;
	private Vector3 originalPos;

	CircleCollider2D rAttack; 

	void Start()
	{
		targetRotation = transform.rotation;
		originalAngle = transform.eulerAngles;
		originalPos = transform.localPosition;

		rAttack = GetComponent<CircleCollider2D>();

	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < rAttack.radius)
		{
			if (Random.Range(0, 100) > 85)
			{
				AltAttack();
			} else if (Random.Range(0, 100) < 15)
			{
				Attack();
			}
		}

		if (doAltAttack)
		{
			timeRotating += Time.deltaTime;
			transform.eulerAngles = new Vector3(0,0, timeRotating*1000+20);
			if(timeRotating > timeAttack)
			{
				timeRotating = 0;
				transform.eulerAngles = originalAngle;
				StopAttack();
			}
		} else if(doAttack)
		{
			Vector3 dir = player.transform.localPosition - transform.localPosition;

			float angle = Mathf.Atan(dir.y/dir.x) * Mathf.Rad2Deg;
			if(dir.x > 0)
			{
				angle += 180;
			}
			transform.eulerAngles = new Vector3(0, 0, originalAngle.z+angle);
		}
		
	}

	public void AltAttack()
	{
		doAltAttack = true;
		doAttack = false;
	}

	public void Attack()
	{
		doAltAttack = false;
		doAttack = true;
	}

	public void StopAttack()
	{
		doAltAttack = false;
		transform.eulerAngles = originalAngle;
	}
}
