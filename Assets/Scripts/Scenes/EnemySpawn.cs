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
		int lastSpawned = 0;
		count = 0;
        bool[] spawns = new bool[spawnLocations.Length];
        for(int i = 0; i < spawns.Length; i ++)
            spawns[i] = false;
        dungeon = GameObject.FindGameObjectWithTag("Room").GetComponent<DungeonLevel>();
        if (dungeon != null)
        {
            while (count < dungeon.Actual.n_enemies)
            {
                //temporal
                System.Random r = new System.Random();
                int n = r.Next(spawnLocations.Length);
				int tries = 0;
                while (tries < 20 && spawns[n])
                {
                    n = r.Next(spawnLocations.Length);
					tries++;
                }
                spawns[n] = true;
				if (whatSpawnPrefab.Length > 0)
				{
					int j = r.Next(whatSpawnPrefab.Length );
					j = j == lastSpawned ? (j + 1) % whatSpawnPrefab.Length : j;

					lastSpawned = j;
					whatSpawnClone = Instantiate(whatSpawnPrefab[j], spawnLocations[n].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
					if (whatSpawnClone.GetComponent<EnemyMovement>() != null)
					{
						whatSpawnClone.GetComponent<EnemyMovement>().startPoint = spawnLocations[n].transform.Find("waypoint0").gameObject;
						whatSpawnClone.GetComponent<EnemyMovement>().endPoint = spawnLocations[n].transform.Find("waypoint1").gameObject;
					}
					whatSpawnClone.SetActive(true);
					
				}
                count++;
            }
        }
        for(int b = 0; b < spawns.Length;b++)
            spawns[b] = false;
                    
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
