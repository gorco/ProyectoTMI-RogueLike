using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour {

    public int attack = 5;
    public float speedAttack = 1.5f;
    private float timeAttack=0;
    private LifeHero lifeH;
    

    // Use this for initialization
    void Start () {
        lifeH = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeHero>();
	}
	
	// Update is called once per frame
	void Update () {
        if(lifeH == null)
		{
			lifeH = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeHero>();
		}
		timeAttack += Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.name == "Player")
        {
			Debug.LogWarning("Me han tocado");
            if (timeAttack >= speedAttack)
            {
                timeAttack = 0;
                lifeH.receiveAttack(attack);
				Debug.Log("Me han dañado. Ahora tengo " + lifeH);
            }
        }

    }
}
