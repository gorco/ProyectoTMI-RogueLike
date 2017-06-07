using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public string nameofnextroom;
    public string thisroom;
    public int next_room;
    public bool closed;
    public bool exist;
    public bool entered = false;
    public int place;
    float time = 0;

   // Use this for initialization
	void Start () {
		
	}

   

    public int Next_room
    {
    get
    {
    return next_room;
    }
    set
    {
        next_room = value;
    }
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Entra player");
        if (coll.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<HeroMovement>().StopMovement();
            GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>().refreshRoom(place, nameofnextroom,gameObject.GetComponent<Door>());
            
        } 
    }
    // Update is called once per frame
    void Update () {
        

	}
}
