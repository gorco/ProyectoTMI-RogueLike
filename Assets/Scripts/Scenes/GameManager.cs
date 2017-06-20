using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour {

    public enum Dungeon { Tutorial = 0, Level1 = 1, Level2 = 2, Level3 = 3, Level4 = 4, Level5 = 5, Level6 = 6, Level7 = 7,
        Level8 = 8, Level9 = 9, Level10 = 10 };
    public Dungeon currentLevel = 0;
    bool newGame = true;
    [SerializeField]
    DungeonLevel currentDungeon = null;
    int n_salas = 5;
    
    GameObject room;


	// Use this for initialization
	void Start () {
        room = GameObject.FindGameObjectWithTag("Room");
        generateDungeon();
	}

    public void changeLevel()
    {


        currentLevel++;
        N_salas += 1;
        if(currentDungeon == null)
            currentDungeon = room.GetComponent<DungeonLevel>();
        if (currentDungeon.map != null)
        {
            currentDungeon.map = new RoomTree<string, Room>();
            currentDungeon.multipleDoorMap = new RoomTree<string, Room>();
            currentDungeon.oneDoorMap = new RoomTree<string, Room>();
            currentDungeon.doorsSaved = new RoomTree<string, bool>();
            currentDungeon.prefabsInRoom = new RoomTree<string, GameObject[]>();
            currentDungeon.ocupadas = new RoomTree<int, int>();//salas que ya tienen puertas, cuantas tienen ocupadas
            currentDungeon.salasCreadas = new RoomTree<string, DoorData[]>();//guardo las salas crafteadas
            currentDungeon.roomsReferenced = new RoomTree<int, List<int>>();//en la habitación K hay una lista del numero de cada habitacion
        }
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.dataPath + "/Scripts/Scenes/Nodes.xml");

        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la lectura del fichero: ", e);
        }
        XmlNodeList nodos = doc.GetElementsByTagName("DoorsData");
        for(int i = 0; i < nodos.Count; i++)
        {
            nodos[i].RemoveAll();
        }
        doc.Save(Application.dataPath + "/Scripts/Scenes/Nodes.xml");


        currentDungeon.GenerateDungeon();
    }

    private void generateDungeon()
    {
        if (newGame)
        {
            if (currentDungeon==null)
            {
                currentDungeon = room.GetComponent<DungeonLevel>();
                currentDungeon.GenerateDungeon();

            }
        }


    }

    
    public int N_salas
    {
        get{
            return n_salas;
        }
        set{
            n_salas = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
		/*switch(currentLevel)
        {
            case Dungeon.Tutorial:
                n_salas = 5;
                break;
            case Dungeon.Level1:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level2:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level3:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level4:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level5:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level6:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level7:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level8:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level9:
                n_salas = n_salas + 1;
                break;
            case Dungeon.Level10:
                n_salas = n_salas + 1;
                break;

        }*/
    }
}
