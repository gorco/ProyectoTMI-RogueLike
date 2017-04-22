using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuest : EventManager
{

	public IState state;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "active quest")
		{
			string var = ((String)ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"", "");

			var value = true;
			this.SetQuest(var, value);
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// active quest
	/// </summary>
	/// <param name="varName"></param>
	/// <param name="varValue"></param>
	private void SetQuest(string varName, bool varValue)
	{
		Debug.Log("ACTIVAR QUEST -->" + varName);
		Type t = state.GetType();
		t.GetProperty(varName).SetValue(state, varValue, null);
	}
}
