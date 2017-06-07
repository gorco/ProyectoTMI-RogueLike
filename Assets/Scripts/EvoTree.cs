using UnityEngine;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System;


public class EvoTree<K,T> : Hashtable{


	private Sprite spr;


	public Sprite Spr
	{
		get
		{
			return spr;
		}
		set
		{
			spr = value;
		}
	}

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
			string nombre =
				nodo.GetAttribute("nombre");

			string info =
				nodo.GetAttribute("info");

			string bonus = 
				nodo.GetAttribute("bonus");

            string cost =
                nodo.GetAttribute("cost");

            string sprite = 
				nodo.GetAttribute("sprite");

			string locked = 
				nodo.GetAttribute("locked");

			string used = 
				nodo.GetAttribute("used");

			string next = 
				nodo.GetAttribute("next");

			string next1 = 
				nodo.GetAttribute("next1");

			EvoNode nuevo = new EvoNode();
			nuevo.Name = nombre;
			nuevo.info = info;
			nuevo.bonus = Int32.Parse(bonus);
            nuevo.cost = Int32.Parse(cost);
			nuevo.sprite = sprite;
			nuevo.locked = ((locked == "true") ? true : false);
			nuevo.used = ((used == "true") ? true : false);
			nuevo.next = next;
			nuevo.next1 = next1;
			this.Add(nuevo.Name, nuevo);

		}
	}

}
