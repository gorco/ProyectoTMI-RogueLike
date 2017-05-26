using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHero : MonoBehaviour {

	public int baseMaxLife = 100;
	public int baseCurrentLife = 100;
	public int baseAttack = 20;
	public float baseSpdAttack = 2;
	public int baseDefense = 5;
	public float baseLuck = 0;
	public float baseWeight = 0;

	[SerializeField]
	private int maxLife;
	[SerializeField]
	private int currentLife;

	internal int getCurrentLife()
	{
		return currentLife;
	}
	[SerializeField]
	private int attack;
	[SerializeField]
	private float spdAttack;
	[SerializeField]
	private int defense;
	[SerializeField]
	private float luck;
	[SerializeField]
	private float weight;

	HeroAttack attackScript;
	HeroMovement movement;

    // Use this for initialization
    void Start()
    {
        this.currentLife = baseMaxLife;
		this.maxLife = baseMaxLife;
		this.attack = baseAttack;
		this.spdAttack = baseSpdAttack;
		this.defense = baseDefense;
		this.luck = baseLuck;
		this.weight = baseWeight;

		attackScript = GetComponentInChildren<HeroAttack>();
		attackScript.setAttack(this.attack);
		attackScript.setSpeedAttack(this.spdAttack);

		movement = GetComponent<HeroMovement>();
		movement.setSpeedfromPeso(this.weight);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void updateStats(int mLife, int atk, float spdAt, int def, float lck, float w, bool set = false)
	{
		if (set)
		{
			this.maxLife = mLife;
			this.attack = atk;
			this.spdAttack = spdAt;
			this.defense = def;
			this.luck = lck;
			this.weight = w;
		} else
		{
			this.maxLife = baseMaxLife + mLife;
			this.attack = baseAttack + atk;
			this.spdAttack = baseSpdAttack + spdAt;
			this.defense = baseDefense + def;
			this.luck = baseLuck + lck;
			this.weight = baseWeight + w;
		}
		attackScript.setAttack(this.attack);
		attackScript.setSpeedAttack(this.spdAttack);
		movement.setSpeedfromPeso(this.weight);
	}

    public void receiveAttack(int attack)
    {
        currentLife -= (attack - defense);
        if (currentLife <= 0)
        {
            currentLife = 0;
        }
        Debug.Log("Vida heroe: "+currentLife);
    }

	public void ObtainLife(int life)
	{
		this.currentLife += life;
		if (currentLife <= 0)
		{
			currentLife = 0;
		}
		if (currentLife > maxLife)
		{
			currentLife = maxLife;
		}
	}
}
