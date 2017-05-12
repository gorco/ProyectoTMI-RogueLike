using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {

    [SerializeField]
    GameObject campoAtaque;
    BoxCollider2D triggerAttack;
   
    EnemyAttack eAtack;
    LifeEnemy lifeE;

    public float danioHero = 10;//ataque
    public float speedAttack = 1;
    

    

    // Use this for initialization
    void Start () {
        lifeE = new LifeEnemy();
        
	}
	
	// Update is called once per frame
	void Update () {
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
        if (other.CompareTag("enemigoSphere"))
        {
            Debug.Log("Hecho el ataque");
            lifeE.life -= danioHero;
            Debug.Log("He hecho daño. Ahora tiene " + lifeE.life);
        }
    }

    
    public void setDanioH(float d)
    {
        danioHero = d;
    }

    public void setSpeedAttack(float s)
    {
        speedAttack = s;
    }
}
