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
    GameObject prefab;
    DungeonLevel level;
    RoomTree<int, bool> ocupadas = new RoomTree<int, bool>();
    bool[] spawns = { false, false, false, false };

    // Use this for initialization
    void Start()
    {
        level = gameObject.GetComponent<DungeonLevel>();

    }

    internal void selectDoor(int num_doors)
    {
        //inicializo las habitaciones libres sin puerta
        for(int g = 0; g < level.map.Count; g++)
        {
            ocupadas[g] = false;
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

            Debug.Log(n.ToString());
            place[i] = n;
            prefab = Instantiate(prefab, spawnLocations[n].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            spawns[n] = true;
            if (level == null)
                level = gameObject.GetComponent<DungeonLevel>();
            level.doors[i] = prefab;//añadimos puerta al nivel
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
            level.doors[i].GetComponent<Door>().nameofnextroom = "room_" + n;
            level.doors[i].GetComponent<Door>().next_room = n;
            level.doors[i].GetComponent<Door>().place = place[i];
        }
    }

    private int choosePosition(int n)
    {
        int x = 0;
     /*   if ((bool)ocupadas[n + 1] == false && (n + 1 < level.map.Count))
            x += 1;
        else if ((bool)ocupadas[n - 1] == false && (n - 1 > 0))
            x -= 1;
        else
        {
 */           
            while (x < level.map.Count)
            {
                if ((bool)ocupadas[x] == true)
                    x++;
                else
                {
                    n = x;
                    break;
                }

            }
     //   }

        return n;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
