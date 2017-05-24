using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomAttack : MonoBehaviour {

    public float danio = 30;
    public string tipo = "normal"; //Valores: {"boom","muerte"}
                                    //boom: quita danio
                                    //muerte: mata al heroe
    LifeHero lifeH;


    // Use this for initialization
    void Start()
    {
        lifeH = new LifeHero();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Debug.Log("Vida antes: " + lifeH.life);
            if (tipo == "muerte")
            {
                lifeH.quitaVida(99999);
                Destroy(gameObject);
            }
            else if (tipo == "boom")
            {
                lifeH.quitaVida(danio);
                Destroy(gameObject);
            }
            
            Debug.Log("Daño real: " + danio + "\n");
            //Debug.Log("Vida: " + lifeH.life);
        }
    }
}
