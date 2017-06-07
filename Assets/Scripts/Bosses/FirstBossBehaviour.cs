using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBehaviour : MonoBehaviour {

    FirstBoss boss;
    Rigidbody2D my_rg;
    GameObject player;
    // Use this for initialization
	void Start () {
        my_rg = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		

	}



}
