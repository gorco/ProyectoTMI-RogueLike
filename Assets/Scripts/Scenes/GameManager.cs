using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum Dungeon { Tutorial = 0, Level1 = 1, Level2 = 2, Level3 = 3, Level4 = 4, Level5 = 5, Level6 = 6, Level7 = 7,
        Level8 = 8, Level9 = 9, Level10 = 10 };
    public Dungeon currentLevel = 0;
    bool newGame = true;
    [SerializeField]
    DungeonLevel currentDungeon = null;
    int n_salas = 3;
    
    GameObject room;


	// Use this for initialization
	void Start () {
        room = GameObject.FindGameObjectWithTag("Room");
        generateDungeon();
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
		switch(currentLevel)
        {
            case Dungeon.Tutorial:
                n_salas = 3;
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

        }
    }
}
