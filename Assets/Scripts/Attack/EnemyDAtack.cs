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

    public Vector3 posPlayer;
    bool lanzar; //Indica si el enemigo ve al jugador
    private Vector3 posInicial;
    private float launch = 0.0f;


    // Use this for initialization
    void Start()
    {
        lifeH = new LifeHero();
        posInicial = arma.transform.position;
        lanzar = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Valor de visto "+lanzar);
        if (lanzar == true)
        {
            arma.transform.Translate(posPlayer*Time.deltaTime);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage") && !lanzar)
        {
            posPlayer = (player.position - arma.transform.position);
            
            Debug.Log("Estoy entrando y no sé por qué");
            lanzar = true;

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.CompareTag("HeroImage") && !lanzar)
        {
            while (launch < 2.0)
            {
                launch += Time.deltaTime;
            }
            
                lanzar = true;
                launch = 0.0f;
                posPlayer = (player.position - arma.transform.position);
            

        }
    }

    public void setVisto()
    {
        Debug.Log("Antes: "+lanzar);
        lanzar = false;
        Debug.Log("Cambio a "+ lanzar);
    }


}
