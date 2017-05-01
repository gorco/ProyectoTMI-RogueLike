using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ////////////////////


//BRUJERIA 1 DE 2

////////////////////////

/// </summary>


public class DungeonLevel : MonoBehaviour {

    private Room actual;
    GameManager gameManager;
    public RoomTree<string, Room> map = null;
   // public RoomTree<string, GameObject> doorMap;
    bool loadLevel = false;
    public GameObject[] doors = new GameObject[4];


    public Room Actual
    {
        get
            {
            return actual;
        }
        set
            {
            actual = value;
        }
    }

    // Use this for initialization
    void Start () {
        if (map == null)
        {
            map = new RoomTree<string, Room>();
  //          map.fillTable(true);


        }
 
    }
	public void GenerateDungeon()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int temporal_doors = 0;//esto son las puertas totales del nivel
        int temporal_mark = gameManager.N_salas-1;//para el random
        int previousN = 0;
        while (temporal_doors < gameManager.N_salas-1)
        {   
            System.Random r = new System.Random();
            int n = r.Next(1, temporal_mark+1);
            if (n == 0) n += 1;
            if (n == previousN)
            {
                if ((n + 1 + temporal_doors) < gameManager.N_salas)
                    n += 1;
                else
                    n -= 1;
            }

            temporal_doors += n;//aumentamos segun el número de puertas que hacemos
            temporal_mark -= n;
            if (map == null)
            {
                map = new RoomTree<string, Room>();
                //          map.fillTable(true);
            }
            Debug.Log(map.Count);
            Room newRoom = new Room("room_" + map.Count ,0, n, (map.Count == 0) ? true : false, gameManager.currentLevel, map.Count);
            if (map.Count == 0)
            {
               

                actual = newRoom;
                actual.name = "room_" + map.Count;
                actual.n_enemies = 0;
                actual.n_doors = n;
                actual.level = gameManager.currentLevel;
            }
                //sala inicial
            map["room_" + map.Count] = newRoom;
            
            //RoomLevel aux = (RoomLevel)map[gameManager.currentLevel];
        }
        while(map.Count < gameManager.N_salas)
        {//si una sala tiene mas de una puerta hay que añadir las salas restantes sin puerta
            Room newRoom = new Room("room_" + map.Count, 0, 0, (map.Count == 0) ? true : false, gameManager.currentLevel, map.Count);
            map["room_" + map.Count] = newRoom;
        }
            
        loadLevel = !loadLevel;
        
    }
	// Update is called once per frame
	void Update () {
        if (loadLevel)
        {
  //          refreshRoom();
            gameObject.GetComponent<RoomLevel>().spawnDoors();
            loadLevel = !loadLevel;
        }
    //    if (actual != gameObject.GetComponent<RoomLevel>())
      //      refreshRoom();
            
	}

    private void changeRoom(int direction, string nameofroom)
    {
        RoomLevel l = gameObject.GetComponent<RoomLevel>();
        
    }//lo haran los triggers salvo la primera
}

public class Room
{
    

    
    public string name;
    public int n_enemies = 3;
    public int n_doors;
    bool isEnabled;
    public GameManager.Dungeon level;
    public int number;


    public Room(string n, int enem, int door, bool first, GameManager.Dungeon l, int numberofroom)
    {
        name = n;
        n_enemies = enem;
        n_doors = door;
        isEnabled = first;
        level = l;
        number = numberofroom;
    }
}