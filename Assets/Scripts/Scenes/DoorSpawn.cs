using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//BRUJERIA 2 DE 2
public class DoorSpawn : MonoBehaviour {

    int count;
    int previousPoint = 0;
    int[] place = new int[4];
    public Transform[] spawnLocations;
    [SerializeField]
    Door newDoor;
    DungeonLevel level;

    bool[] spawns = { false, false, false, false };//puertas activadas

    // Use this for initialization
    void Start()
    {
        level = gameObject.GetComponent<DungeonLevel>();

    }


    internal void selectDoor(int num_doors, Door door, int number)
    {
        if(door != null && level.salasCreadas.ContainsKey(door.nameofnextroom))
        {  //desactivo todas las puertas
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
            {
                g.SetActive(false);
            }

            Door[] doorsFromRoom = (Door[])level.salasCreadas[door.nameofnextroom];
            foreach(Door d in doorsFromRoom)
            {
                if (d != null)
                {
                  
                    Door aux = d;
                    switch (d.place)
                    {
                        case 0:
                            newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 0;
                            newDoor.enabled = true;
                            break;
                        case 1:
                            newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 1;
                            newDoor.enabled = true;
                            break;
                        case 2:
                            newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 2;
                            newDoor.enabled = true;
                            break;
                        case 3:
                            newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();

                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 3;
                            newDoor.enabled = true;
                            break;
                    }
                    spawns[door.place] = true;//Marco como ocupada
                    newDoor.gameObject.SetActive(true);
                    level.doors[0] = newDoor;
                    Debug.Log("Puerta en " + door.place.ToString());
                }
                
                //level.ocupadas[level.Actual.number] = true;//marco la sala ocupada
                //level.Actual.n_doors -= 1;
            }
        }
        else
        {
            if (level.ocupadas.Count == 0)
            {
                for (int g = 0; g < level.map.Count; g++)
                {
                    level.ocupadas[g] = false;
                }
            }
            //desactivo todas las puertas
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
            {
                g.SetActive(false);
            }
            Door[] createdDoors = new Door[4];
            //inicializo las habitaciones libres sin puerta
            for (int b = 0; b < spawns.Length; b++)
                spawns[b] = false;
            if (door != null)//calculo la puerta opuesta: 0:N, 1:E, 2:S, 3:W
            {
                switch (door.place)
                {
                    case 0:
                        newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 2;
                        newDoor.enabled = true;
                        break;
                    case 1:
                        newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 3;
                        newDoor.enabled = true;
                        break;
                    case 2:
                        newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 0;
                        newDoor.enabled = true;
                        break;
                    case 3:
                        newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 1;
                        newDoor.enabled = true;
                        break;
                }
                spawns[newDoor.place] = true;//Marco como ocupada
                newDoor.gameObject.SetActive(true);
                level.doors[0] = newDoor;
                Debug.Log("Puerta en " + newDoor.ToString());
                level.ocupadas[level.Actual.number] = true;//marco la sala ocupada
                //level.Actual.n_doors -= 1;
                num_doors--;
                createdDoors[door.place] = newDoor;
            }

            //necesito crear el resto de puertas de la sala con n_doors
            for (int i = 0; i < num_doors/*level.Actual.n_doors-1*/; ++i)
            {
                System.Random r = new System.Random();
                int n = r.Next(0, 4);
                while (spawns[n])//necesito un spawn donde no haya puerta
                {
                    n = r.Next(0, 4);
                   // Debug.Log(n);
                }
                //escojo el indice de la siguiente sala a generar
                int j = r.Next(level.map.Count);
                level.ocupadas[0] = true;
                if ((bool)level.ocupadas[j] == true)
                    j = choosePosition(j);
                level.ocupadas[j] = true;
                //creo las nuevas puertas en la posicion n
                //y el la nextroom j
                switch (n)
                {
                    case 0:
                        newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + j;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = j;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 1:
                        newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + j;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = j;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 2:
                        newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + j;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = j;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 3:
                        //level.transform.FindChild("DoorWestEast").gameObject.SetActive(true);
                        newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + j;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = j;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                }
                newDoor.gameObject.SetActive(true);
                place[i] = n;
                spawns[n] = true;
                createdDoors[n] = newDoor;
                if (level == null)
                    level = gameObject.GetComponent<DungeonLevel>();
                if (door != null)
                    level.doors[i + 1] = newDoor;//añadimos puerta al nivel
                else
                    level.doors[i] = newDoor;

            }
            level.salasCreadas[level.Actual.name] = createdDoors;

        }




    }
    /*
    internal void selectDoor(int num_doors, Door door, int number)
    {
        //inicializo las habitaciones libres sin puerta
        for(int b = 0; b < spawns.Length;b++)
            spawns[b] = false;
        for(int g = 0; g < level.map.Count; g++)
        {
            ocupadas[g] = false;
        }
        if (door != null)
        {
            switch (door.place)
            {
                case 0:
                    door.place = 2;
                    break;
                case 1:
                    door.place = 3;
                    break;
                case 2:
                    door.place = 0;
                    break;
                case 3:
                    door.place = 1;
                    break;
            }
            prefab = Instantiate(prefab, spawnLocations[door.place].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            spawns[door.place] = true;
            level.doors[0] = prefab;
            Debug.Log("Puerta en " +door.place.ToString());
            level.doors[0].GetComponent<Door>().nameofnextroom = level.nameofpreviousroom;
            level.doors[0].GetComponent<Door>().thisroom = level.Actual.name;
            level.doors[0].GetComponent<Door>().next_room = number;//movida porque hay que poner el indice de actual.name
            level.doors[0].GetComponent<Door>().place = door.place;
            level.doors[0].GetComponent<Door>().enabled = true;
            level.doors[0].GetComponent<BoxCollider2D>().enabled = true;
        }
        ocupadas[level.Actual.number] = true;
        count = 0;
        for (int i = 0; i < num_doors; ++i)
        {

            System.Random r = new System.Random();
            int n = r.Next(0,4);
            while (spawns[n])
            {
                n = r.Next(0,4);
                Debug.Log(n);
            }


            place[i] = n;
            prefab = Instantiate(prefab, spawnLocations[n].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            spawns[n] = true;
            if (level == null)
                level = gameObject.GetComponent<DungeonLevel>();
            if(door != null)
                level.doors[i+1] = prefab;//añadimos puerta al nivel
            else
                level.doors[i] = prefab;
            count++;
        }
        int[] aux = new int[level.doors.Length];
        int cont = 0;
        for (int d = 0; d < level.doors.Length; d++)
        {
            if (level.doors[d] != null)
                cont++;
        }
        
        for(int i = 0; i < cont; i++)
        {
            System.Random r = new System.Random();
            int n = r.Next(level.map.Count);
         
            if ((bool)ocupadas[n] == true)
                n = choosePosition(n);
            //Debug.Log(level.doors[i].GetComponent<Door>());
            ocupadas[n] = true;
            if(door != null )
            {
                if(i != 0)
                {
                    level.doors[i].GetComponent<Door>().thisroom = level.Actual.name;
                    level.doors[i].GetComponent<Door>().nameofnextroom = "room_" + n;
                    level.doors[i].GetComponent<Door>().next_room = n;
                    level.doors[i].GetComponent<Door>().place = place[i];
                    level.doors[i].GetComponent<Door>().enabled = true;
                    level.doors[i].GetComponent<BoxCollider2D>().enabled = true;
                }
                
            }
            else
            {
                level.doors[i].GetComponent<Door>().thisroom = level.Actual.name;
                level.doors[i].GetComponent<Door>().nameofnextroom = "room_" + n;
                level.doors[i].GetComponent<Door>().next_room = n;
                level.doors[i].GetComponent<Door>().place = place[i];
                level.doors[i].GetComponent<Door>().enabled = true;
                level.doors[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
    }
    */
    private int choosePosition(int n)
    {
        int x = 0;
        while (x < level.map.Count)
        {
            if ((bool)level.ocupadas[x] == true)
                x++;
            else
            {
                n = x;
                break;
            }

        }

        return n;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
