using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaArrojadiza : MonoBehaviour {
    public float velocidad = 5.0F;
    private LifeHero lH;
    public float danioArma = 3;
    //public Transform player;
    private EnemyDAtack eda;
    Vector2 posIni;


	// Use this for initialization
	void Start () {

        lH = new LifeHero();
        eda = new EnemyDAtack();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage") || collision.CompareTag("wall"))
        {
            eda.setVisto();
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
