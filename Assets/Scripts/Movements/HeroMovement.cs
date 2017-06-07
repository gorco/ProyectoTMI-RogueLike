using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroMovement : MonoBehaviour {

	private float speed; //Velocidad de movimiento

    private int dir=1; //posición donde mira 1:arriba, 2: abajo, 3: dcha, 4: izda

	public GameObject weapon;
	public Sprite up;
	public Sprite right;
	public Sprite down;
	public Sprite left;

	private SpriteRenderer sprite;

	private int xDir = 1;
	private int yDir = 1;

    // Use this for initialization
    void Start () {
		speed = 12;
		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0)//izda
        {
			//Debug.Log("izda");
			sprite.sprite = left;
            //transform.Rotate(Vector3.back, -90);
            if (dir == 1)
				weapon.transform.Rotate(Vector3.back, -90);
            else if (dir == 2)
				weapon.transform.Rotate(Vector3.back, 90);
            else if (dir == 3)
				weapon.transform.Rotate(Vector3.back, 180);
            dir = 4;
			xDir = Mathf.FloorToInt(Input.GetAxis("Horizontal")); ;
		}
        if (Input.GetAxis("Horizontal") > 0)//dcha
        {
			//Debug.Log("dcha");
			sprite.sprite = right;
			if (dir == 1)
				weapon.transform.Rotate(Vector3.back, 90);
            else if (dir == 2)
				weapon.transform.Rotate(Vector3.back, -90);
            else if (dir == 4)
				weapon.transform.Rotate(Vector3.back, 180);
            dir = 3;
			xDir = Mathf.CeilToInt(Input.GetAxis("Horizontal")); ;
		}
            
        if (Input.GetAxis("Vertical") > 0)//arriba
        {
			sprite.sprite = up;
			Quaternion newRot = transform.rotation;
            //Debug.Log(newRot);
            newRot.z = 0.0f;
			weapon.transform.rotation = newRot;
            //Debug.Log(newRot);
            dir = 1;
			yDir = Mathf.CeilToInt(Input.GetAxis("Vertical")); ;
		}
           
        if (Input.GetAxis("Vertical") < 0)//abajo
        {
			sprite.sprite = down;
			//Debug.Log("pabajo");
			Quaternion newRot = transform.rotation;
            //Debug.Log(newRot);
            newRot.z = 180.0f;
			weapon.transform.rotation = newRot;
            //Debug.Log(newRot);
            dir = 2;
			yDir = Mathf.FloorToInt(Input.GetAxis("Vertical"));
        }

        transform.Translate(speed * Math.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * xDir, speed * Math.Abs(Input.GetAxis("Vertical"))*Time.deltaTime * yDir, 0);
        transform.Translate(speed * Math.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * xDir, speed * Math.Abs(Input.GetAxis("Vertical")) * Time.deltaTime * yDir, 0);
        
    }

    public void setSpeedfromPeso(float peso)
    {
        float v = 12 - (peso / 100)*12;
		setSpeed(v);
	}

    public void setSpeed(float v)
    {
		speed = v;
		if (v > 12)
            this.speed = 12;
        if (v < 1)
			this.speed = 1;

    }

    public void StopMovement()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    

    
}
