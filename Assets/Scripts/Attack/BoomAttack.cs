using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomAttack : MonoBehaviour {

    public int attack = 30;
    public string tipo = "normal"; //Valores: {"boom","muerte"}
								   //boom: quita danio
								   //muerte: mata al heroe
	private LifeHero lifeH;


    // Use this for initialization
    void Start()
    {
        lifeH = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeHero>();
	}

	// Update is called once per frame
	void Update () {
		if(lifeH == null)
			lifeH = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeHero>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Debug.Log("Vida antes: " + lifeH.life);
            if (tipo == "kill")
            {
                lifeH.receiveAttack(99999);
                Destroy(gameObject);
            }
            else
            {
                lifeH.receiveAttack(attack);
                Destroy(gameObject);
            }
            
            Debug.Log("Daño real: " + attack + "\n");
            
        }
    }
}
