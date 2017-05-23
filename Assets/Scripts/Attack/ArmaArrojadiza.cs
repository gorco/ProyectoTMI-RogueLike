using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaArrojadiza : MonoBehaviour {
    public float velocidad = 5.0F;
    private LifeHero lH;
    public float danioArma = 3;
    //public Transform player;
    //private DistanceAttack da;
    Vector2 posIni;


	// Use this for initialization
	void Start () {

        lH = new LifeHero();
       // da = new DistanceAttack();
        posIni = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage") || collision.CompareTag("wall"))
        {
          
            gameObject.GetComponentInParent<DistanceAttackEnemy>().setDisparoFalse();
            if (collision.CompareTag("HeroImage"))
                lH.quitaVida(danioArma);

            transform.position = posIni;            
        }
    }

    public void setDanioArma(float danArma)
    {
        danioArma = danArma;
    }

    public void setV(float v)
    {
        velocidad = v;
    }
}
