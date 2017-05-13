using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDAtack : MonoBehaviour {

    public Transform player; //jugador
    public Transform arma; //Arma del enemigo
    public float timeAttack = 1.5f;//Indica la velocidad con la que dispara

    public GameObject flechaPrefab;

    public LifeHero lifeH;

    private Vector2 posPlayer;
    private bool visto = false; //Indica si el enemigo ve al jugador
    private Vector2 posInicial; 


    // Use this for initialization
    void Start()
    {
        lifeH = new LifeHero();
        posInicial = arma.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (visto == true)
        {
           
            arma.transform.position = Vector2.MoveTowards(arma.transform.position, posPlayer, Time.deltaTime);
            
            if ((Vector2)arma.transform.position == posPlayer)
            {
                arma.transform.position = posInicial;
                visto = false;
            }
        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage"))
        {
            Debug.Log("He visto al enemigo. Voy a disparar");
            posPlayer = player.position;
            visto = true;
        }

    }

    
}
