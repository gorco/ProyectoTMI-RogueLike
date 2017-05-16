using UnityEngine;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System;


public class RoomTree<K,T> : Hashtable{



	public void fillTable(bool isOrganic)
	{

		XmlDocument doc = new XmlDocument ();
		try{
			doc.Load (Application.dataPath+"/Scripts/Nodes.xml");
           
		}
		catch(XmlException e){
			throw new XmlException("Fallo en la lectura del fichero: ", e);
		}


		XmlNodeList nodos = doc.GetElementsByTagName ("Nodos");

		XmlNodeList list = (isOrganic ?  ((XmlElement)nodos [0]).GetElementsByTagName ("Organic") : ((XmlElement)nodos [0]).GetElementsByTagName ("Cyborg"));

		foreach(XmlElement nodo in list)
		{
			string levelId =
				nodo.GetAttribute("levelId");

			string n_habitacion =
				nodo.GetAttribute("n_habitacion");

			string habitaciones = 
				nodo.GetAttribute("habitaciones");


			string locked = 
				nodo.GetAttribute("locked");

			string used = 
				nodo.GetAttribute("used");

			string next = 
				nodo.GetAttribute("next");

			string next1 = 
				nodo.GetAttribute("next1");

			RoomNode nuevo = new RoomNode();
			nuevo.LevelId = levelId;
			nuevo.N_habitacion = Int32.Parse(n_habitacion);
			nuevo.Habitaciones = Int32.Parse(habitaciones);
			nuevo.locked = ((locked == "true") ? true : false);
			nuevo.used = ((used == "true") ? true : false);
			nuevo.next = next;
			nuevo.next1 = next1;
			this.Add(nuevo.LevelId, nuevo);

		}
	}

}
