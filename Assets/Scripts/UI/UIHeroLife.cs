using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroLife : MonoBehaviour {

	public Text life;
	LifeHero heroLife;

	// Use this for initialization
	void Start () {
		heroLife = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeHero>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		life.text = "Hero: " +  heroLife.getCurrentLife();
	}
}
