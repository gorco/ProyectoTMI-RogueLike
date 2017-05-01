using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public string nameofnextroom;
    public int next_room;
    public bool closed;
    public bool exist;

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
        if (coll.CompareTag("Player"))
        {
            //GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>().changeRoom(next_room,nameofnextroom);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
