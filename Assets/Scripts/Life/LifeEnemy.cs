using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LifeEnemy : MonoBehaviour {

    public float maxLife = 70;
	public float defense = 1;

    private Animator anim;
    private Animation an;

	[SerializeField]
    private float life = 70.0f;

	// Use this for initialization
	void Start () {
        an = GetComponentInParent<Animation>();
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
            //.SetTrigger("die");
            an.Play("die");
            
            if (this.transform.parent != null)
                if (an.IsPlaying("die"))
				    Destroy(this.transform.parent.gameObject);
			else
                if (an.IsPlaying("die"))
				    Destroy(this.gameObject);
		}
		
        Debug.Log("Vida enemigo: "+life);
    }

    
}
