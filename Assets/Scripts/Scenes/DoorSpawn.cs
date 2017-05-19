using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;
//BRUJERIA 2 DE 2
public class DoorSpawn : MonoBehaviour {

    int count;
    int previousPoint = 0;
    int[] place = new int[4];
    public Transform[] spawnLocations;
    [SerializeField]
    Door newDoor;
    DungeonLevel level;
    EnemySpawn chanchan;
    int countOfMultipleRoomsFilled = 0;

    bool[] spawns = { false, false, false, false };//puertas activadas

    // Use this for initialization
    void Start()
    {
        level = gameObject.GetComponent<DungeonLevel>();
        if(level.multipleDoorMap.ContainsKey("room_"+0))
            countOfMultipleRoomsFilled = 1;
    }

    public void saving(string nameofnextroom)
    {
        Door nuevo = null;
        XmlDocument doc = new XmlDocument();
  /*      try
        {
            doc.Load(Application.dataPath + "/Scripts/Scenes/Nodes.xml");
        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la escritura del fichero: ", e);
        }*/
        System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(DoorData));

        System.IO.StreamWriter file = new System.IO.StreamWriter(
            Application.dataPath + "/Scripts/Scenes/Nodes.xml");
        DoorData[] auxiliar = (DoorData[])level.salasCreadas[nameofnextroom];
        foreach(DoorData door in auxiliar)
            writer.Serialize(file, door);
        file.Close();
        
    }


    public void loading(bool isOrganic)
    {
        DoorData nuevo = null;
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.dataPath + "/Scripts/Nodes.xml");

        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la lectura del fichero: ", e);
        }

        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Door));
        System.IO.StreamReader file = new System.IO.StreamReader(Application.dataPath + "/Scripts/Nodes.xml");
        nuevo = new DoorData();
        nuevo = (DoorData)reader.Deserialize(file);
        DoorData[] temporalDoors;
        if (level.salasCreadas.ContainsKey(nuevo.thisroom))
        {
            temporalDoors = (DoorData[])level.salasCreadas[nuevo.thisroom];
            DoorData[] combined = new DoorData[temporalDoors.Length + 1];
            Array.Copy(temporalDoors, combined, temporalDoors.Length);
            Array.Copy(temporalDoors, temporalDoors.Length, combined, temporalDoors.Length, temporalDoors.Length+1);
            level.salasCreadas.Add(nuevo.thisroom, combined);
        }
        else
        {
            level.salasCreadas.Add(nuevo.thisroom, nuevo);
        }

        
    }




    internal void selectDoor(int num_doors, Door door, int number)
    {
        int auxiliar = 0;
        
            //si paso por una puerta y ya se ha creado la sala
        if (door != null && level.salasCreadas.ContainsKey(door.nameofnextroom))
        {  //desactivo todas las puertas
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
            {
                g.SetActive(false);
            }
            for (int i = 0; i < spawns.Length; i++)
                spawns[i] = false;
            //habilito las puertas de esa sala
            DoorData[] doorsFromRoom = (DoorData[])level.salasCreadas[door.nameofnextroom];
            foreach(DoorData d in doorsFromRoom)
            {
                
                if (d != null)
                {
                  
                    DoorData aux = d;
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
                    spawns[newDoor.place] = true;//Marco como ocupada
                    newDoor.gameObject.SetActive(true);
                    level.doors[0] = newDoor;
                    Debug.Log("Puerta en " + door.place.ToString());
                }
                
                //level.ocupadas[level.Actual.number] = true;//marco la sala ocupada
                //level.Actual.n_doors -= 1;
            }
            foreach (GameObject prefab in (GameObject[])level.prefabsInRoom[door.thisroom])
            {
                if (prefab != null)
                    prefab.SetActive(true);
            }

        }
        else
        {//si no he creado ninguna sala
            if (level.ocupadas.Count == 0)
            {
                for (int g = 0; g < level.map.Count; g++)
                {
                    level.ocupadas[g] = 0;//todas las salas tienen 0 puertas construidas
                }
                /*foreach (Room room in level.multipleDoorMap.Values)
                {
                    level.ocupadas[room.number] = (int)level.ocupadas[room.number] + 1;
                }*/
               // level.ocupadas[0] = 1;
            }
            //desactivo todas las puertas
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
            {
                g.SetActive(false);
            }
            Door[] createdDoors = new Door[4];
            //meto los objetos/enemigos
            chanchan = gameObject.GetComponent<EnemySpawn>();
            chanchan.spawn();
            //inicializo las habitaciones libres sin puerta
            for (int b = 0; b < spawns.Length; b++)
                spawns[b] = false;

            if (door != null)//calculo la puerta opuesta: 0:N, 1:E, 2:S, 3:W
            {
                //metodaco chulo chulo chulaco
                saving(newDoor.nameofnextroom);
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
                level.ocupadas[level.Actual.number] = (int)level.ocupadas[level.Actual.number]+1;//marco la sala ocupada añadiendo uno al contador de puertas que la referencian

                //level.Actual.n_doors -= 1;
                num_doors--;
                createdDoors[door.place] = newDoor;
            }
            int ifIsZero = 0;
            int newIndex = 0 ;
            //necesito crear el resto de puertas de la sala con n_doors
            for (int i = 0; i < num_doors; ++i)
            {
                System.Random r = new System.Random();
                int n = r.Next(0, 4);
                while (spawns[n])//necesito un spawn donde no haya puerta
                {
                    n = r.Next(0, 4);
                   // Debug.Log(n);
                }
                //escojo el indice de la siguiente sala a generar
                if (level.multipleDoorMap.ContainsKey("room_" + 0) && level.Actual.number == 0)
                {
                    ifIsZero += 1;
                    level.ocupadas[0] = (int)level.ocupadas[0] + 1;//aumento en uno las puertas ocupadas
                    newIndex = ifIsZero;
                }
                else
                {
                    newIndex = choosePosition();
                    //level.ocupadas[newIndex] = (int)level.ocupadas[newIndex] + 1;//aumento en uno las puertas ocupadas
                    level.ocupadas[level.Actual.number] = (int)level.ocupadas[level.Actual.number] + 1;
                }
                

                //creo las nuevas puertas en la posicion n
                //y el la nextroom newIndex
                switch (n)
                {
                    case 0:
                        newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + newIndex;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 1:
                        newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + newIndex;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 2:
                        newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + newIndex;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                        newDoor.place = n;
                        newDoor.enabled = true;
                        break;
                    case 3:
                        //level.transform.FindChild("DoorWestEast").gameObject.SetActive(true);
                        newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = "room_" + newIndex;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
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
    private int choosePosition()
    {
        Room aux = null;
        System.Random r = new System.Random();
        int j = 0;
        bool encontrado = false;
        int actualIndex = 1000;
        if (level.multipleDoorMap.ContainsKey(level.Actual.name))
        {
            Room room = (Room)level.multipleDoorMap[level.Actual.name];
            actualIndex = room.number;
        }
        while (encontrado == false)
        {
            if (j == actualIndex)
                j = r.Next(level.map.Count);
            else
            {
                if ((countOfMultipleRoomsFilled == level.multipleDoorMap.Count) || ((level.multipleDoorMap.Count -1 == countOfMultipleRoomsFilled) && (actualIndex == level.Actual.number)))
                {
                    if (level.oneDoorMap.ContainsKey("room_" + j))
                    {
                        aux = (Room)level.oneDoorMap["room_" + j];
                        if (aux.n_doors > (int)level.ocupadas[j])//si la sala tiene mas puertas que las que se han creado aun
                            encontrado = true;
                        else
                            j = r.Next(level.map.Count);
                    }
                    else
                        j = r.Next(level.map.Count);
                }
                else if (level.multipleDoorMap.ContainsKey("room_" + j))
                {
                    aux = (Room)level.multipleDoorMap["room_" + j];
                    if (aux.n_doors > (int)level.ocupadas[j])//si la sala tiene mas puertas que las que se han creado aun
                    {
                        encontrado = true;
                        if (aux.n_doors == (int)level.ocupadas[j] + 1)
                            countOfMultipleRoomsFilled += 1;
                    }
                    else
                        j = r.Next(level.map.Count);
                }
                else
                    j = r.Next(level.map.Count);

            }
        }
            
        return j;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
