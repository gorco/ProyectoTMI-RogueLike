using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSprites : MonoBehaviour {

	public Sprite[] npcsSprites;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = npcsSprites[Random.Range(0, npcsSprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
