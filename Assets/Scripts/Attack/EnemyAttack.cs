using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour {

    public float danioEnemy = 5;
    public float vTimeAttack = 1.5f;
    float timeAttack=0;
    LifeHero lifeH;
    

    // Use this for initialization
    void Start () {
        lifeH = new LifeHero();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage"))
        {
            
            timeAttack -= Time.deltaTime;
            if (timeAttack >= 0)
            {
                timeAttack = 0;
                lifeH.life -= danioEnemy;
                Debug.Log("Me han dañado. Ahora tengo " + lifeH.life);
            }
        }

    }
}
