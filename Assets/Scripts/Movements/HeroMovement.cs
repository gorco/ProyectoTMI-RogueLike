using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroMovement : MonoBehaviour {
    public float speed = 10.0F; //Velocidad de movimiento
    private int dir=1; //posición donde mira 1:arriba, 2: abajo, 3: dcha, 4: izda
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0 && dir != 4)//izda
        {
            Debug.Log("izda");
            
            //transform.Rotate(Vector3.back, -90);
            if (dir == 1)
                transform.Rotate(Vector3.back, -90);
            else if (dir == 2)
                transform.Rotate(Vector3.back, 90);
            else if (dir == 3)
                transform.Rotate(Vector3.back, 180);
            dir = 4;

        }
        else if (Input.GetAxis("Horizontal") > 0 && dir !=3)//dcha
        {
            Debug.Log("dcha");
           
            if (dir == 1)
                transform.Rotate(Vector3.back, 90);
            else if (dir == 2)
                transform.Rotate(Vector3.back, -90);
            else if (dir == 4)
                transform.Rotate(Vector3.back, 180);
            dir = 3;

        }
            
        else if (Input.GetAxis("Vertical") > 0 && dir != 1)//arriba
        {
            
            Quaternion newRot = transform.rotation;
            //Debug.Log(newRot);
            newRot.z = 0.0f;
            transform.rotation = newRot;
            //Debug.Log(newRot);
            dir = 1;

        }
           
        else if (Input.GetAxis("Vertical") < 0 && dir != 2 )//abajo
        {
            Debug.Log("pabajo");
             Quaternion newRot = transform.rotation;
             Debug.Log(newRot);
             newRot.z = 180.0f;
             transform.rotation = newRot;
             Debug.Log(newRot);
            dir = 2;
        }
        transform.Translate(0, speed * Math.Abs(Input.GetAxis("Vertical"))*Time.deltaTime, 0);
        transform.Translate(0, speed * Math.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime, 0);
        
    }

    

    
}
