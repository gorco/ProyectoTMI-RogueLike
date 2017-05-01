using UnityEngine;
using System.Collections;

public class RoomNode 
{

	private string levelId;//id del nivel del dungeon
	public int n_habitacion;//numero de habitacion del nivel
	public int habitaciones;//habitaciones totales del nivel
	//si esta bloqueado el poder
	public bool locked;
	//si esta adquirido el poder
	public bool used;
	//los dos nuevos poderes que libera
	public string next;
	public string next1;

	public string LevelId {
		get 
		{
			return levelId;
		}
		set 
		{
			levelId = value;
		}
	}
	public int N_habitacion
	{
		get
		{
			return n_habitacion;
		}
		set
		{
			n_habitacion = value;
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




	public int Habitaciones
	{
		get
		{
			return habitaciones;
		}
		set
		{
			habitaciones = value;
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

