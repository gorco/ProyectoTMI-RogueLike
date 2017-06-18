using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowDialog : MonoBehaviour
{

	public string fieldName;
	public bool throwAtStart = false;
	private bool canTalk = false;
	private bool talking = false;

	// Use this for initialization
	void Start () {
		if (throwAtStart)
		{
			ThrowDialogNow();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!talking && Input.GetKeyDown(KeyCode.E) == true)
		{
			talking = true;
			ThrowDialogNow();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		canTalk = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		canTalk = false;
		talking = false;
	}

	public void ThrowDialogNow()
	{
		talking = true;
		GetComponentInParent<ObjectsWithDialogsManager>().startDialog(fieldName);
	}
}
