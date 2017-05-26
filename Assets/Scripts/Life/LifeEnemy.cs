using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEnemy : MonoBehaviour {

    public float maxLife = 70;
	public float defense = 1;

	[SerializeField]
    private float life = 70.0f;

	// Use this for initialization
	void Start () {
        
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
			if (this.transform.parent != null)
				Destroy(this.transform.parent.gameObject);
			else
				Destroy(this.gameObject);
		}
		
        Debug.Log("Vida enemigo: "+life);
    }
}
