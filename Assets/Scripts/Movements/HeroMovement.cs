using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {
    public float speed = 10.0F; //Velocidad de movimiento
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
    }

    

    
}
