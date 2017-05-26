using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {

    [SerializeField]
    GameObject campoAtaque;
    BoxCollider2D triggerAttack;
   
    private float speedAttack = 1;
	private int attack = 0;

	private bool canAttack = true;

	[SerializeField]
	private float time = 0;

    // Use this for initialization
    void Start () {        
	}
	
	// Update is called once per frame
	void Update () {

		if (!canAttack)
		{
			time += Time.deltaTime;
			if (time > speedAttack)
			{
				canAttack = true;
			}
		}

        if (Input.GetButtonDown("Fire1"))
        {

            campoAtaque.transform.Rotate(new Vector3(0, 0, 90));// * Time.deltaTime * speedAttack);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            campoAtaque.transform.Rotate(new Vector3(0, 0, -90));// * Time.deltaTime * speedAttack);
		}

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canAttack && other.CompareTag("enemigoSphere"))
        {
            Debug.Log("Hecho el ataque ENTER");
			other.gameObject.GetComponent<LifeEnemy>().receiveAttack(this.attack);

			canAttack = false;
			time = 0;
        }
    }

	void OnTriggerStay2D(Collider2D collision)
	{
		if (canAttack && collision.CompareTag("enemigoSphere"))
		{
			Debug.Log("Hecho el ataque STAY");
			collision.gameObject.GetComponent<LifeEnemy>().receiveAttack(this.attack);

			canAttack = false;
			time = 0;
		}
	}


	public void setSpeedAttack(float s)
    {
        speedAttack = s;
    }

	internal void setAttack(int attack)
	{
		this.attack = attack;
	}
}
