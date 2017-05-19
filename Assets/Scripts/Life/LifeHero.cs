using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHero : MonoBehaviour {

    public float maxLife = 100.0f;
    private float life;
    public float defensa = 0;
    

    // Use this for initialization
    void Start()
    {
        
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitaVida(float danio)
    {
        life -= danio;
        if (life <= 0)
        {
            life = 0;
            
        }
        Debug.Log("Vida heroe: "+life);
    }
}
