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
    int isDoorCreated = 0;
    string s;
    public Transform[] spawnLocations;
    [SerializeField]
    Door newDoor;
    DoorData doorToAppend = new DoorData();
    DoorData doorToAppend1 = new DoorData();
    DoorData doorToAppend2 = new DoorData();
    DoorData doorToAppend3 = new DoorData();

    DungeonLevel level;
    EnemySpawn chanchan;
    ItemSpawn items;
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
        try
        {
            doc.Load(Application.dataPath + "/Scripts/Scenes/Nodes.xml");
        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la escritura del fichero: ", e);
        }
        DoorData[] auxiliar = null;
        auxiliar = (DoorData[])level.salasCreadas[nameofnextroom];
        string s = null;
        foreach (DoorData door in auxiliar)
        {
            if (door != null && !level.doorsSaved.ContainsKey(door.thisroom))
            {
                s = door.thisroom; 
                //XmlNode root = doc.CreateElement("DoorsData");
                //doc.AppendChild(root);
                XmlNode root =
                doc.SelectSingleNode("DoorsData");
                XmlNode rootNode = doc.CreateElement("DoorData");
                XmlAttribute attribute = doc.CreateAttribute("nameofnextroom");
                attribute.Value = door.nameofnextroom;
                rootNode.Attributes.Append(attribute);
                XmlAttribute attribute2 = doc.CreateAttribute("thisroom");
                attribute2.Value = door.thisroom;
                rootNode.Attributes.Append(attribute2);
                XmlAttribute attribute3 = doc.CreateAttribute("next_room");
                attribute3.Value = door.next_room.ToString();
                rootNode.Attributes.Append(attribute3);
                XmlAttribute attribute4 = doc.CreateAttribute("place");
                attribute4.Value = door.place.ToString();
                rootNode.Attributes.Append(attribute4);
                root.AppendChild(rootNode);
                doc.Save(Application.dataPath + "/Scripts/Scenes/Nodes.xml");
 

            }
        }
        level.doorsSaved.Add(s, true);
        doc.Save(Application.dataPath + "/Scripts/Scenes/Nodes.xml");



        /*System.Xml.Serialization.XmlSerializer writer =
          new System.Xml.Serialization.XmlSerializer(typeof(DoorData));
        
        System.IO.StreamWriter file;
        if (System.IO.File.Exists(Application.dataPath + "/Scripts/Scenes/Nodes.xml"))
        {
            file = new System.IO.StreamWriter(Application.dataPath + "/Scripts/Scenes/Nodes.xml", append: true);
        }
        else
        {
            file = new System.IO.StreamWriter(Application.dataPath + "/Scripts/Scenes/Nodes.xml");
        }
            
        string s = nameofnextroom;
        DoorData[] auxiliar = null; //(DoorData[])level.salasCreadas.Values;
//        auxiliar = (DoorData[])level.salasCreadas.Values;
        auxiliar = (DoorData[])level.salasCreadas[nameofnextroom];
        foreach(DoorData door in auxiliar)
            writer.Serialize(file, door);
        file.Close();
        */

    }


    public void loading(string room)
    {

        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.dataPath + "/Scripts/Scenes/Nodes.xml");

        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la lectura del fichero: ", e);
        }
        XmlNodeList nodos = doc.GetElementsByTagName("DoorData");
        List<DoorData> newDoors = new List<DoorData>();
        foreach (XmlElement nodo in nodos)
        {
            string thisroom = nodo.GetAttribute("thisroom");
            if (thisroom == room)
            {
                string nameofnextroom = nodo.GetAttribute("nameofnextroom");
                string nextroom = nodo.GetAttribute("next_room");
                string place = nodo.GetAttribute("place");
                DoorData door = new DoorData();
                door.nameofnextroom = nameofnextroom;
                door.thisroom = thisroom;
                door.next_room = Int32.Parse(nextroom);
                door.place = Int32.Parse(place);
                newDoors.Add(door);
            }
            

        }
        if (level.salasCreadas.ContainsKey(room))
        {
            level.salasCreadas.Remove(room);
            level.salasCreadas.Add(room, newDoors.ToArray());
        }
            /* DoorData nuevo = null;
         XmlDocument doc = new XmlDocument();

         System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(DoorData));
         System.IO.StreamReader file = new System.IO.StreamReader(Application.dataPath + "/Scripts/Scenes/Nodes.xml");

         DoorData temporalDoors = new DoorData();
        /* using (var sr = new System.IO.StreamReader(Application.dataPath + "/Scripts/Scenes/Nodes.xml"))
         {
             var xs = new System.Xml.Serialization.XmlSerializer(temporalDoors.GetType());
             temporalDoors = (DoorsData)(xs.Deserialize(sr));
         }*/

        //if (level.salasCreadas.ContainsKey(room))
        //{

        /*  temporalDoors = (DoorData[])level.salasCreadas[room];
          DoorData[] combined = new DoorData[temporalDoors.Length + 1];
          Array.Copy(temporalDoors, combined, temporalDoors.Length);
          Array.Copy(temporalDoors, temporalDoors.Length, combined, temporalDoors.Length, temporalDoors.Length+1);
          level.salasCreadas.Add(nuevo.thisroom, combined);*/
        //}
        /* else
         {
             level.salasCreadas.Add(nuevo.thisroom, nuevo);
         }*/


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
            DoorData[] doorsFromRoom = null;
            loading(door.nameofnextroom);//
            doorsFromRoom = (DoorData[])level.salasCreadas[door.nameofnextroom];
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
                            doorToAppend.nameofnextroom = d.nameofnextroom;
                            doorToAppend.thisroom = d.thisroom;
                            doorToAppend.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            doorToAppend.place = 0;
                            //newDoor.enabled = true;
                            break;
                        case 1:
                            newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 1;
                            doorToAppend1.nameofnextroom = d.nameofnextroom;
                            doorToAppend1.thisroom = d.thisroom;
                            doorToAppend1.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            doorToAppend1.place = 1;

                            //newDoor.enabled = true;
                            break;
                        case 2:
                            newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 2;
                            doorToAppend2.nameofnextroom = d.nameofnextroom;
                            doorToAppend2.thisroom = d.thisroom;
                            doorToAppend2.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            doorToAppend2.place = 2;

                            //newDoor.enabled = true;
                            break;
                        case 3:
                            newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();

                            newDoor.nameofnextroom = d.nameofnextroom;
                            newDoor.thisroom = d.thisroom;
                            newDoor.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            newDoor.place = 3;
                            doorToAppend3.nameofnextroom = d.nameofnextroom;
                            doorToAppend3.thisroom = d.thisroom;
                            doorToAppend3.next_room = d.next_room;//movida porque hay que poner el indice de actual.name
                            doorToAppend3.place = 3;

                            //newDoor.enabled = true;
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
            }
            //desactivo todas las puertas
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
            {
                g.SetActive(false);
            }
            DoorData[] createdDoors = new DoorData[4];
            //meto los objetos/enemigos
            chanchan = gameObject.GetComponent<EnemySpawn>();
            items = gameObject.GetComponent<ItemSpawn>();
            chanchan.spawn();
//            items.spawn();
            //inicializo las habitaciones libres sin puerta
            for (int b = 0; b < spawns.Length; b++)
                spawns[b] = false;

            if (door != null)//calculo la puerta opuesta: 0:N, 1:E, 2:S, 3:W
            {
                isDoorCreated = 1;
                switch (door.place)
                {
                    case 0:
                        newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 2;
                        newDoor.enabled = true;
                        doorToAppend.nameofnextroom = level.nameofpreviousroom;
                        doorToAppend.thisroom = level.Actual.name;
                        doorToAppend.next_room = number;//movida porque hay que poner el indice de actual.name
                        doorToAppend.place = 2;
                        break;
                    case 1:
                        newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 3;
                        newDoor.enabled = true;
                        doorToAppend1.nameofnextroom = level.nameofpreviousroom;
                        doorToAppend1.thisroom = level.Actual.name;
                        doorToAppend1.next_room = number;//movida porque hay que poner el indice de actual.name
                        doorToAppend1.place = 3;
                        break;
                    case 2:
                        newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 0;
                        newDoor.enabled = true;
                        doorToAppend2.nameofnextroom = level.nameofpreviousroom;
                        doorToAppend2.thisroom = level.Actual.name;
                        doorToAppend2.next_room = number;//movida porque hay que poner el indice de actual.name
                        doorToAppend2.place = 0;
                        break;
                    case 3:
                        newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                        newDoor.nameofnextroom = level.nameofpreviousroom;
                        newDoor.thisroom = level.Actual.name;
                        newDoor.next_room = number;//movida porque hay que poner el indice de actual.name
                        newDoor.place = 1;
                        newDoor.enabled = true;
                        doorToAppend3.nameofnextroom = level.nameofpreviousroom;
                        doorToAppend3.thisroom = level.Actual.name;
                        doorToAppend3.next_room = number;//movida porque hay que poner el indice de actual.name
                        doorToAppend3.place = 1;
                        break;
                }
                spawns[newDoor.place] = true;//Marco como ocupada
                newDoor.gameObject.SetActive(true);
                level.doors[0] = newDoor;
                List<int> l = (List<int>)level.roomsReferenced[level.Actual.number];
                if (door.place == 0)
                {
                    createdDoors[2] = doorToAppend;
                    if (!l.Contains(doorToAppend.next_room))
                        l.Add(doorToAppend.next_room);
                }
                   
                else if (door.place == 1)
                {
                    createdDoors[3] = doorToAppend1;
                    if (!l.Contains(doorToAppend1.next_room))
                        l.Add(doorToAppend.next_room);
                }
                    
                else if (door.place == 2)
                {
                    createdDoors[0] = doorToAppend2;
                    if (!l.Contains(doorToAppend2.next_room))
                        l.Add(doorToAppend.next_room);
                }
                    
                else if (door.place == 3)
                {
                    createdDoors[1] = doorToAppend3;
                    if (!l.Contains(doorToAppend3.next_room))
                        l.Add(doorToAppend.next_room);
                }
                    
                Debug.Log("Puerta en " + newDoor.ToString());
               // level.ocupadas[level.Actual.number] = (int)level.ocupadas[level.Actual.number]+1;//marco la sala ocupada añadiendo uno al contador de puertas que la referencian
                level.roomsReferenced.Remove(level.Actual.number);
                level.roomsReferenced.Add(level.Actual.number,l);

                //level.Actual.n_doors -= 1;
                num_doors--;
                //createdDoors[door.place] = doorToAppend;
            }

            int ifIsZero = 0;
            int newIndex = 0 ;
            //elijo todos los indices de golpe

            theChoosens(isDoorCreated);

            //necesito crear el resto de puertas de la sala con n_doors
            foreach (int i in level.Actual.indexOfRooms)
            {
                if(level.nameofpreviousroom != "room_"+i)//si no he hecho ya su puerta
                {
                    System.Random r = new System.Random();
                    int n = r.Next(0, 4);
                    while (spawns[n])//necesito un spawn donde no haya puerta
                    {
                        n = r.Next(0, 4);
                        // Debug.Log(n);
                    }
                    newIndex = i;// choosePosition(createdDoors);
                    switch (n)
                    {
                        case 0:
                            newDoor = level.transform.FindChild("DoorNorth").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = "room_" + newIndex;
                            newDoor.thisroom = level.Actual.name;
                            newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            newDoor.place = n;
                            newDoor.enabled = true;
                            doorToAppend.nameofnextroom = "room_" + newIndex;
                            doorToAppend.thisroom = level.Actual.name;
                            doorToAppend.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            doorToAppend.place = n;
                            break;
                        case 1:
                            newDoor = level.transform.FindChild("DoorEast").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = "room_" + newIndex;
                            newDoor.thisroom = level.Actual.name;
                            newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            newDoor.place = n;
                            newDoor.enabled = true;
                            doorToAppend1.nameofnextroom = "room_" + newIndex;
                            doorToAppend1.thisroom = level.Actual.name;
                            doorToAppend1.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            doorToAppend1.place = n;
                            break;
                        case 2:
                            newDoor = level.transform.FindChild("DoorSouth").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = "room_" + newIndex;
                            newDoor.thisroom = level.Actual.name;
                            newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            newDoor.place = n;
                            newDoor.enabled = true;
                            doorToAppend2.nameofnextroom = "room_" + newIndex;
                            doorToAppend2.thisroom = level.Actual.name;
                            doorToAppend2.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            doorToAppend2.place = n;
                            break;
                        case 3:
                            //level.transform.FindChild("DoorWestEast").gameObject.SetActive(true);
                            newDoor = level.transform.FindChild("DoorWest").gameObject.GetComponent<Door>();
                            newDoor.nameofnextroom = "room_" + newIndex;
                            newDoor.thisroom = level.Actual.name;
                            newDoor.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            newDoor.place = n;
                            newDoor.enabled = true;
                            doorToAppend3.nameofnextroom = "room_" + newIndex;
                            doorToAppend3.thisroom = level.Actual.name;
                            doorToAppend3.next_room = newIndex;//movida porque hay que poner el indice de actual.name
                            doorToAppend3.place = n;

                            break;
                    }
                    s = newDoor.thisroom;
                    newDoor.gameObject.SetActive(true);
                    place[n] = n;
                    spawns[n] = true;
                    if (n == 0)
                        createdDoors[n] = doorToAppend;
                    else if (n == 1)
                        createdDoors[n] = doorToAppend1;
                    else if (n == 2)
                        createdDoors[n] = doorToAppend2;
                    else if (n == 3)
                        createdDoors[n] = doorToAppend3;

                    if (level == null)
                        level = gameObject.GetComponent<DungeonLevel>();
                    if (door != null)
                        level.doors[(n + 1) % 4] = newDoor;//añadimos puerta al nivel
                    else
                        level.doors[n] = newDoor;
                }
           

            }
            //metodaco chulo chulo chulaco
            
            level.salasCreadas[level.Actual.name] = createdDoors;
            saving(s);
        }


    

    }

    private void theChoosens(int isDoorCreated)
    {
        System.Random r = new System.Random();
        int n = r.Next(0, level.map.Count);
        List<Room> rooms = new List<Room>();
        bool encontrado = false;
        foreach (Room auxi in level.multipleDoorMap.Values)
        {
            if(auxi.n_doors != auxi.indexOfRooms.Count)
                rooms.Add(auxi);
        }
        if (rooms.Contains(level.Actual))
            rooms.Remove(level.Actual);
        List<Room> allRooms = new List<Room>();
        foreach (Room auxi in level.map.Values)
        {
            if (auxi.n_doors != auxi.indexOfRooms.Count)
                allRooms.Add(auxi);
        }
        if (allRooms.Contains(level.Actual))
            allRooms.Remove(level.Actual);
        //calculamos todos los indices de las puertas de la habitación
        for (int i = 0; i < level.Actual.n_doors- isDoorCreated; i++)
        {   //si no es mi habitacion
            if (true)
            {   //si es la primera que genero, que sea multiple
                if (i == 0)
                {
                    foreach (Room aux in rooms)
                    {   //si se puede usar
                        if (level.ocupadas.ContainsKey(aux.number))
                        {//si no es la mia(donde estoy)
                            if (aux.number != level.Actual.number)
                            {
                                //si no la tengo en la sala
                                if (!level.Actual.indexOfRooms.Contains(aux.number))
                                {//marco como ocupada
                                    level.ocupadas[aux.number] = (int)level.ocupadas[aux.number] + 1;
                                    if ((int)level.ocupadas[aux.number] == aux.n_doors)//si ya he ocupado todas sus puertas
                                        level.ocupadas.Remove(aux.number);
                                    level.Actual.indexOfRooms.Add(aux.number);//la añado en la mia y a la que voy
                                    Room refresh = (Room)level.map["room_" + aux.number];
                                    refresh.indexOfRooms.Add(level.Actual.number);
                                    level.map["room_" + aux.number] = refresh;
                                    //level.Actual.indexOfRooms.Add();
                                    encontrado = true;
                                    break;
                                    
                                }
                            }

                        }
                   
                    }
                    if(!encontrado)
                    {
                        foreach (Room aux in allRooms)
                        {   //si se puede usar
                            if (level.ocupadas.ContainsKey(aux.number))
                            {//si no es la mia(donde estoy)
                                if (aux.number != level.Actual.number)
                                {
                                    //si no la tengo en la sala
                                    if (!level.Actual.indexOfRooms.Contains(aux.number))
                                    {//marco como ocupada
                                        level.ocupadas[aux.number] = (int)level.ocupadas[aux.number] + 1;
                                        if ((int)level.ocupadas[aux.number] == aux.n_doors)//si ya he ocupado todas sus puertas
                                            level.ocupadas.Remove(aux.number);
                                        level.Actual.indexOfRooms.Add(aux.number);//la añado en la mia y a la que voy
                                        Room refresh = (Room)level.map["room_" + aux.number];
                                        refresh.indexOfRooms.Add(level.Actual.number);
                                        level.map["room_" + aux.number] = refresh;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                   

                    if (true)
                    {   //si es la primera que genero, que sea multiple
                        
                            foreach (Room aux in allRooms)
                            {   //si se puede usar
                                if (level.ocupadas.ContainsKey(aux.number))
                                {//si no es la mia(donde estoy)
                                    if (aux.number != level.Actual.number)
                                    {
                                        //si no la tengo en la sala
                                        if (!level.Actual.indexOfRooms.Contains(aux.number))
                                        {//marco como ocupada
                                            level.ocupadas[aux.number] = (int)level.ocupadas[aux.number] + 1;
                                            if ((int)level.ocupadas[aux.number] == aux.n_doors)//si ya he ocupado todas sus puertas
                                                level.ocupadas.Remove(aux.number);
                                            level.Actual.indexOfRooms.Add(aux.number);//la añado en la mia y a la que voy
                                            Room refresh = (Room)level.map["room_" + aux.number];
                                            refresh.indexOfRooms.Add(level.Actual.number);
                                            level.map["room_" + aux.number] = refresh;
                                            encontrado = true;
                                            break;
                                        }
                                    }
                                }

                            }
                        
                    }
                }

            }
            else
                n = r.Next(0, level.map.Count);
        }
}

    private int choosePosition(DoorData[] createdDoors)
    {//level.ocupadas se borra cuando se llena
        Room aux = null;
        System.Random r = new System.Random();
        int j = 0;
        int i = 0;
        bool encontrado = false;
        int actualIndex = 1000;
        System.Random rand = new System.Random();
        int n = rand.Next(0, level.map.Count);
        int[] salasEnSala = new int[createdDoors.Length];
        for(int ix = 0; ix < createdDoors.Length; ix++)
        {
            if (createdDoors[ix] != null)
                salasEnSala[ix] = createdDoors[ix].next_room;
        }
        while (encontrado == false)
        {
            if (level.Actual.number != n)
            {   //si room tiene una puerta
                if (level.oneDoorMap.Contains(level.Actual.name))
                {  // miro a ver si le quedan puertas por crear
                    if (level.ocupadas.Contains(n) && level.multipleDoorMap.ContainsKey("room_"+n))
                    {
                        actualizaListas(ref encontrado, rand, ref n);
                    }
                    else
                        n = rand.Next(0, level.map.Count);

                }
                else
                {//si room 0 no tiene referencias y tiene mas de una puerta me da igual
                 //crear alguna con una puerta pero tengo que crear al menos una con varias
                    
                    if ((int)level.ocupadas[level.Actual.number] == 0)
                    {
                        List<int> salasMultiplesTotales = new List<int>();//(int[])level.multipleDoorMap.Keys;
                        foreach (Room auxi in level.multipleDoorMap.Values)
                        {//necesito el indice, no el nombre de la sala
                            salasMultiplesTotales.Add(auxi.number);
                        }
                      //  if()
                        if(level.multipleDoorMap.Count >1)
                        {
                            foreach (int s in level.ocupadas.Keys)
                            {
                                if (salasMultiplesTotales.Contains(s) && s != level.Actual.number)
                                {
                                    n = s;
                                    break;
                                }

                            }
                        }
                        

                        while (level.ocupadas.Contains(n) && !level.multipleDoorMap.ContainsKey("room_" + n))
                            n = rand.Next(0, level.map.Count);
                    }
                    actualizaListas(ref encontrado, rand, ref n);
                }
            }
            else
            {
                n = rand.Next(0, level.map.Count);
            }
        }




        return n;
    }

    private void actualizaListas(ref bool encontrado, System.Random rand, ref int n)
    {
        //marco que la habitacion n lleva una puerta mas referenciada
        Room room = (Room)level.map["room_"+n];
        if ((int)level.ocupadas[n] < room.n_doors)//si hay menos puertas ocupadas
        {
            List<int> l = (List<int>)level.roomsReferenced[level.Actual.number];
            if (!l.Contains(n))//si la habitacion a la que voy no tiene ya una puerta (dos puertas a room 2 en la misma sala
            {
                level.ocupadas[n] = (int)level.ocupadas[n] + 1;
                if ((int)level.ocupadas[n] == room.n_doors)//si ya he ocupado todas sus puertas
                    level.ocupadas.Remove(n);
                encontrado = true;//encontre lo que queria
                l.Add(n);
                level.roomsReferenced[level.Actual.number] = l;
            }
            else
                n = rand.Next(0, level.map.Count);//calculo otro indice
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
    /*
    private int choosePosition(DoorData[] createdDoors)
    {
        Room aux = null;
        System.Random r = new System.Random();
        int j = 0;
        int i = 0;
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
                if (createdDoors != null)
                {
                    foreach(DoorData d in createdDoors)
                    {
                        if(d != null)
                        {
                            if (d.place == j)
                            {
                                encontrado = false;
                                j = r.Next(level.map.Count);
                            }
                        }
                            
                    }
                }
            }
        }
            
        return j;
    }*/


    // Update is called once per frame
    void Update()
    {
       
    }
}
