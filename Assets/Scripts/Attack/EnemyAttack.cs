using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour {

    int danioEnemy = 5;
    float timeAttack = 0.0f;
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
            Debug.Log("Voy a por ti");
            timeAttack += Time.deltaTime;
            if (timeAttack >= 1.5)
            {
                timeAttack = 0;
                lifeH.life -= danioEnemy;
                Debug.Log("Me han dañado. Ahora tengo " + lifeH.life);
            }
        }

    }
}
