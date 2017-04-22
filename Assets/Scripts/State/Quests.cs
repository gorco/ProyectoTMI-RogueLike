using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : IState {

	[SerializeField]
	public bool RedQuest { get; set; }

	[SerializeField]
	public bool BlueQuest { get; set; }

	[SerializeField]
	public int BlueClicks { get; set; }

	[SerializeField]
	public int RedClicks { get; set; }

	private void Start()
	{
		RedQuest = false;
		BlueQuest = false;
	}
}
