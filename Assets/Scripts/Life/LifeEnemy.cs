using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LifeEnemy : MonoBehaviour {

    public float maxLife = 70;
	public float defense = 1;

    private Animator anim;
    

	[SerializeField]
    private float life = 70.0f;

	// Use this for initialization
	void Start () {
        
        anim = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void receiveAttack(int attack)
    {
        life -= (attack - defense);
        if (life <= 0)
        {
            life = 0;
            anim.SetTrigger("die");
            Invoke("destroyEnemy", 0.5f);
		}
		
        Debug.Log("Vida enemigo: "+life);
    }

    private void destroyEnemy()
    {
        if (this.transform.parent != null)
            Destroy(this.transform.parent.gameObject);
        else
            Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>().Actual.n_enemies--;
    }
    
	public float GetCurrentLife()
	{
		return life;
	}
}
