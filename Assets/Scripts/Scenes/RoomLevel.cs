using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLevel : MonoBehaviour {


    public int n_doors;
    GameManager gameManager;
    public int n_enemies = 3;
    string name;
    bool isEnabled;
    public GameObject[] doors = new GameObject[4];
    DoorSpawn spawner;
    public GameManager.Dungeon level;


  

   

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        

    }

    public void spawnDoors(Door r, int n)
    {
        n_doors = gameObject.GetComponent<DungeonLevel>().Actual.n_doors;
        spawner = gameObject.GetComponent<DoorSpawn>();
//        for (int i = 0; i < n_doors; ++i)
  //      {
            spawner.selectDoor(n_doors,r, n);//creo n_doors puertas

    //    }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
