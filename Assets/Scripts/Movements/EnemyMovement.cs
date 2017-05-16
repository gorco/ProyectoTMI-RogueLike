using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject startPoint;
    public GameObject endPoint;

    public Transform player;

    private bool final = false;//indica si va al endPoint (false) o al startoint (true)

    public float speed = 1.0F; //Velocidad de movimiento

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () { 
        if (!final)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);
            if (transform.position == endPoint.transform.position)
                final = true;
        }
        
        if (final)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, speed * Time.deltaTime);
            if (transform.position == startPoint.transform.position)
                final = false;
        }

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HeroImage"))
        {
            //Debug.Log("Lo veo");
           if (!final)
            {
                endPoint.transform.position = player.position;
                
            }
            else
            {
                startPoint.transform.position = player.position;
              
            }
        }
    }

}
