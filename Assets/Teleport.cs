
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().changeLevel();
            this.enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
