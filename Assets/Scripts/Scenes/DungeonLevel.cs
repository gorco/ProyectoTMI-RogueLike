﻿using System;
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
    public string nameofpreviousroom;
    GameManager gameManager;
    public RoomTree<string, Room> map = null; //mapa de todas las salas
    public RoomTree<string, Room> oneDoorMap = new RoomTree<string, Room>();//contiene las salas que solo tienen una puerta
    public RoomTree<string, Room> multipleDoorMap = new RoomTree<string, Room>();//contiene las salas que tienen varias puertas
    public RoomTree<int, int> ocupadas = new RoomTree<int, int>();//salas que ya tienen puertas, cuantas tienen ocupadas
    public RoomTree<string, DoorData[]> salasCreadas = new RoomTree<string, DoorData[]>();//guardo las salas crafteadas
    public RoomTree<string, GameObject[]> prefabsInRoom = new RoomTree<string, GameObject[]>();//guardo enemigos y objetos en sala
    public RoomTree<int, List<int>> roomsReferenced = new RoomTree<int, List<int>>();//en la habitación K hay una lista del numero de cada habitacion
    public RoomTree<string, bool> doorsSaved = new RoomTree<string, bool>();//guardo las salas con puertas guardadas
    public bool loadLevel = false;
    public int totalDoors = 0;
    public bool isBossDefeated = false;
    public Door[] doors = new Door[4];
    GameObject room;
    int p;
    string nameofroom;
    bool executed = false;

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
    

    public void refreshRoom(int place, string name, Door door)
    {
        p = place;
        nameofroom = name;
       switch(place)
        {
            case 0:
                p = 2;
                break;
            case 1:
                p = 3;
                break;
            case 2:
                p = 0;
                break;
            case 3:
                p = 1;
                break;
        }
        Door r = null;
        //esto es para guardar los items y enemigos de la sala
        GameObject[] aux = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] aux2 = GameObject.FindGameObjectsWithTag("Enemies");
        GameObject[] combined = new GameObject[aux.Length + aux2.Length];
        Array.Copy(aux, combined, aux.Length);
        Array.Copy(aux2, 0, combined, aux.Length, aux2.Length);
        foreach (GameObject prefab in combined)
            prefab.SetActive(false);
        prefabsInRoom[actual.name] = combined;
        Room newRoom = (Room)map[nameofroom];
        nameofpreviousroom = actual.name;
        int n = actual.number;
        actual = newRoom;
        //actual.name = "room_" + map.Count;
        actual.n_enemies = newRoom.n_enemies;
        //activo los prefabs que estan en la habitacion
        if((GameObject[])prefabsInRoom[actual.name] != null)
        {
            foreach (GameObject g in (GameObject[])prefabsInRoom[actual.name])
                g.SetActive(true);
        }      
        gameObject.GetComponent<RoomLevel>().spawnDoors(door, n);//cargo las puertas de nuevo
        Transform newPlayerPos = null;

        switch (p)
        {
            case 0:
                newPlayerPos = room.transform.Find("North");
                newPlayerPos.position = new Vector2(room.transform.Find("North").position.x, room.transform.Find("North").position.y - 0.25f);

                break;
            case 1:
                newPlayerPos = room.transform.Find("East");
                newPlayerPos.position = new Vector2(room.transform.Find("East").position.x - 0.25f, room.transform.Find("East").position.y);

                break;
            case 2:
                newPlayerPos = room.transform.Find("South");
                newPlayerPos.position = new Vector2(room.transform.Find("South").position.x, room.transform.Find("South").position.y + 0.25f);

                break;
            case 3:
                newPlayerPos = room.transform.Find("West");
                newPlayerPos.position = new Vector2(room.transform.Find("West").position.x + 0.25f, room.transform.Find("East").position.y);

                break;
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = newPlayerPos.position;

    }

    // Use this for initialization
    void Start () {
        if (map == null)
        {
            map = new RoomTree<string, Room>();
            //          map.fillTable(true);
            if (ocupadas.Count == 0)
            {
                for (int g = 0; g < map.Count; g++)
                {
                    ocupadas[g] = false;
                }
            }


        }
        
 
    }
	public void GenerateDungeon()
    {
        if (map == null)
        {
            map = new RoomTree<string, Room>();
            //          map.fillTable(true);
        }
        if (room == null)
        {
            GameObject.FindGameObjectWithTag("Room");
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int temporal_doors = 0;//esto son las puertas totales del nivel creadas
        int n_salas = gameManager.N_salas;//numero de salas que hay
        int previousN = 0;
        int doorsInMultipleRooms = 2;
        int roomsOneDoor = 2;
        int n_salascreadas = 0;
        int max_doors = 5;
        int isFirstOneDoor = 0;
        totalDoors = gameManager.N_salas + gameManager.N_salas - 2;
        if (gameManager.currentLevel == GameManager.Dungeon.Tutorial)
            max_doors = 5;
        else
            max_doors += 1;
/*        else if (gameManager.currentLevel == GameManager.Dungeon.Level1)
            max_doors = 3;
        else max_doors = 4;
*/      while (temporal_doors < gameManager.N_salas+gameManager.N_salas - 2)
        {   if(gameManager.N_salas - roomsOneDoor-n_salascreadas >0)
            {
                System.Random r = new System.Random();
                int n;

                if (map.Count == 0)
                {
                    n = r.Next(1, 5);
                    isFirstOneDoor =((n == 1) ? 1 : 0);
                }
                    
                else n = r.Next(2, max_doors + 1);
                if (n == 1)
                    roomsOneDoor -= 1;
                if (n == previousN && (n + 1 <= max_doors)) n += 1;


                int enemies = r.Next(1, n_salascreadas + 1);
                if (n > 2)
                {
                    if (n == 3)
                        roomsOneDoor += 1;
                    else if (n == 4)
                        roomsOneDoor += 2;

                }
                int centinel = n;//por si cambia n
                while (gameManager.N_salas + gameManager.N_salas - 2 < 2*n +temporal_doors - isFirstOneDoor)
                {
                    n -= 1;
                    n = r.Next(2, n);
                }

                if (n == 2)
                    if (centinel == 3)
                        roomsOneDoor -= 1;
                    else if(n==4)
                        roomsOneDoor -= 2;
                Room newRoom = new Room("room_" + map.Count, enemies, map.Count, n, (map.Count == 0) ? true : false, ((map.Count + 1 == gameManager.N_salas)) ? true : false, gameManager.currentLevel, map.Count, (map.Count == 0 && (int)gameManager.currentLevel % 2 !=0) ? 1 : 0);
                if (map.Count == 0)
                {
                    actual = newRoom;
                    actual.name = "room_" + map.Count;
                    actual.n_enemies = 0;
                    actual.n_doors = n;
                    actual.level = gameManager.currentLevel;
                    actual.number = 0;
                    actual.npc = newRoom.npc;
                }
                if (n == 1)//si solo tiene una puerta
                    oneDoorMap["room_" + map.Count] = newRoom;
                else
                    multipleDoorMap["room_" + map.Count] = newRoom;
                map["room_" + map.Count] = newRoom;
                n_salascreadas += 1;
                temporal_doors += n;
                doorsInMultipleRooms += 1;
                previousN = n;
            }

            else
            {
                System.Random r = new System.Random();
                int enemies = r.Next(1, n_salascreadas);

                Room newRoom = new Room("room_" + map.Count, enemies, map.Count, 1, (map.Count == 0) ? true : false, ((map.Count +1 == gameManager.N_salas)) ? true : false, gameManager.currentLevel, map.Count, (map.Count == 0 && (int)gameManager.currentLevel % 2 != 0) ? 1 : 0);
                oneDoorMap["room_" + map.Count] = newRoom;
                map["room_" + map.Count] = newRoom;
                temporal_doors += 1;
                n_salascreadas += 1;
                roomsOneDoor -= 1;

            }


        }
 
        for(int ind = 0; ind < map.Count; ind++)
        {
            roomsReferenced.Add(ind, new List<int>());
        }
        loadLevel = !loadLevel;

        temporal_doors = 0;
        n_salascreadas = 0;
        roomsOneDoor = 0;
        doorsInMultipleRooms = 0;
    }
	// Update is called once per frame
	void Update () {
        if(room == null)
        {
            room = GameObject.FindGameObjectWithTag("Room");
        }
        if (loadLevel)
        {
  //         aqui hago el spawn de puertas
            gameObject.GetComponent<RoomLevel>().spawnDoors(null,1000);

            loadLevel = !loadLevel;
        }
        if(actual.isLast && !executed)
        {
            executed = true;
            //isBossDefeated = true;


            if (isBossDefeated)
                GameObject.FindGameObjectWithTag("Teleport").GetComponent<CircleCollider2D>().enabled = true;
                GameObject.FindGameObjectWithTag("Teleport").GetComponent<Teleport>().enabled = true;
        }
            
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
    public int n_items;
    bool isEnabled;
    public bool isLast;
    public GameManager.Dungeon level;
    public int number;
    public int npc;
    public List<int> indexOfRooms = new List<int>();


    public Room(string n, int enem, int items, int door, bool first, bool last, GameManager.Dungeon l, int numberofroom, int newnpc)
    {
        name = n;
        n_enemies = enem;
        n_doors = door;
        isEnabled = first;
        n_items = items;
        isLast = last;
        level = l;
        number = numberofroom;
        npc = newnpc;
    }

    public int Number()
    {
        return number;
    }
}