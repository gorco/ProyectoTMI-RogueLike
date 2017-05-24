using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttackEnemy : MonoBehaviour {
    public Transform arma;
    public Transform player;
    public float recarga = 1.5f; 

    private bool disparo;
    private Vector2 posIni;
    private Vector2 posPlayer;
    private float time;
    // Use this for initialization
    void Start()
    {
        posIni = arma.transform.position;
        disparo = false;
        time = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (disparo)
        {
            arma.Translate(posPlayer * Time.deltaTime);
        }
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !disparo)
        {
            posPlayer = (player.position - arma.transform.position);

            Debug.Log("Estoy entrando y no sé por qué");
            disparo = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !disparo)
        {
            time += Time.deltaTime;
            if (time>=recarga)
            {
                time = 0.0f;
                posPlayer = (player.position - arma.transform.position);
                disparo = true;
            }
        }
    }

    public void setDisparoFalse()
    {
        disparo = false;
    }
    
}
