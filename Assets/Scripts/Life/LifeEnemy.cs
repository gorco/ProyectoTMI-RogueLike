using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour {

    public float maxLife = 70;
    private float life = 70.0f;
    GameObject r;


	// Use this for initialization
	void Start () {
        r = GameObject.Find("Rival");
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void quitaVida(float danio)
    {
        life -= danio;
        if (life <= 0)
        {
            life = 0;
            Destroy(r);
        }
        Debug.Log("Vida enemigo: "+life);
    }
}
