using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {

    [SerializeField]
    GameObject campoAtaque;
    BoxCollider2D triggerAttack;

    //Transform Enemy;
    EnemyAttack eAtack;
    int danioHero = 10;
    LifeEnemy lifeE;

    

    // Use this for initialization
    void Start () {
        lifeE = new LifeEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemigoSphere"))
        {
            Debug.Log("puedo atacar");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("enemigoSphere"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                lifeE.life -= danioHero;
                Debug.Log("He hecho daño. Ahora tiene "+ lifeE.life);

            }

            
        }
    }
}
