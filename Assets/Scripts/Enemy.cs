using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public string color;
	public UnityEngine.UI.Text red;
	public UnityEngine.UI.Text blue;
	public Quests quest;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnPointerClick(PointerEventData eventData)
	{

	}

	public void OnPointerDown(PointerEventData eventData)
	{

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(color == "red" && quest.RedQuest)
		{
			quest.RedClicks = quest.RedClicks + 1;
			red.text = quest.RedClicks.ToString();
		} else if (color == "blue" && quest.BlueQuest)
		{
			quest.BlueClicks = quest.BlueClicks + 1;
			blue.text = quest.BlueClicks.ToString();
		}
	}
}