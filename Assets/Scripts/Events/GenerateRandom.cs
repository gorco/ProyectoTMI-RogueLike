using Isometra;
using System;
using UnityEngine;

public class GenerateRandom : EventManager {

	public IState state;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if(ev.Name.Replace("\"", "") == "generate random")
		{
			string var = ((String)ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"", "");

			this.Random(var);
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Set random value in Globalstate
	/// </summary>
	private void Random(string varName)
	{
		Type t = state.GetType();
		int varValue = UnityEngine.Random.Range(0, 100);
		Debug.Log("RANDOM = "+ varValue);
		t.GetProperty(varName).SetValue(state, varValue, null);
	}
}
