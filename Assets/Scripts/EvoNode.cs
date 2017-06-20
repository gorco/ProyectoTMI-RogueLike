using UnityEngine;
using System.Collections;

public class EvoNode 
{

	private string name;
	public string info;
	public int bonus;
    public int cost;
	public string sprite;
	//si esta bloqueado el poder
	public bool locked;
	//si esta adquirido el poder
	public bool used;
	//los dos nuevos poderes que libera
	public string next;
	public string next1;

	public string Info {
		get 
		{
			return info;
		}
		set 
		{
			info = value;
		}
	}
	public int Bonus
	{
		get
		{
			return bonus;
		}
		set
		{
			bonus = value;
		}
	}
    public int Cost
    {
        get
        {
            return cost;
        }
        set
        {
            cost = value;
        }
    }
    public string Sprite 
	{
		get 
		{
			return sprite;
		}
		set 
		{
			sprite = value;
		}
	}
	public bool Locked
	{
		get
		{
			return locked;
		}
		set
		{
			locked = value;
		}

	}
	public bool Used
	{
		get
		{
			return used;
		}
		set
		{
			used = value;
		}
		
	}
	public string Next {
		get 
		{
			return next;
		}
		set 
		{
			next = value;
		}
	}

	public string Next1 {
		get 
		{
			return next1;
		}
		set 
		{
			next1 = value;
		}
	}




	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}

	}

	/**public description Description{
	
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}**/



}

