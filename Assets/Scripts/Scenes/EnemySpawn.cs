using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    [SerializeField]
    //int n_enemies;
    int count;
    int previousPoint = 0;
    DungeonLevel dungeon;
    public Transform[] spawnLocations;
    public GameObject[] whatSpawnPrefab;
    public GameObject whatSpawnClone;
    //GameObject prefab;


    public void spawn()
    {
        count = 0;
        dungeon = GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>();
        if (dungeon != null)
        {
            while (count < dungeon.Actual.n_enemies+dungeon.Actual.n_items)
            {
               //temporal
                System.Random r = new System.Random();
                int n = r.Next(spawnLocations.Length-1);
                int j = r.Next(whatSpawnPrefab.Length - 1);
                Debug.Log("creo coso en "+n);
                whatSpawnClone = Instantiate(whatSpawnPrefab[j], spawnLocations[n].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                whatSpawnClone.SetActive(true);
                count++;
            }
        }
                    
    }


	// Use this for initialization
	void Start () {
        dungeon = GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>();
        /* count = 0;
         if(dungeon != null && dungeon.loadLevel)
         {
             for (int i = 0; i < dungeon.Actual.n_enemies; ++i)
             {

                 System.Random r = new System.Random();
                 int n = r.Next(3);
                 if (previousPoint == n)
                 {
                     n = (n + 1) % 3;
                     previousPoint = n;
                 }

                 Debug.Log(n.ToString());
                 spawn(n);
                 count++;
             }
         }*/


    }

    // Update is called once per frame
    void Update () {
        if (dungeon == null)
            dungeon = GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>();

    }
}
