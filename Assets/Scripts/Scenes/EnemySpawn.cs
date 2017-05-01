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
    public GameObject[] whatSpawnClone;
    //GameObject prefab;


    void spawn(int n)
    {
        
        whatSpawnClone[0] = Instantiate(whatSpawnPrefab[0],spawnLocations[n].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
            
    }


	// Use this for initialization
	void Start () {
        dungeon = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DungeonLevel>();
        count = 0;
        for(int i = 0; i < dungeon.Actual.n_enemies; ++i)
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
       
    }
	
	// Update is called once per frame
	void Update () {
        while (count < dungeon.Actual.n_enemies)
        {
            System.Random r = new System.Random();
            int n = r.Next(3);
            if (dungeon.Actual.n_enemies < dungeon.Actual.n_enemies / 4)
                spawn(n);
        }
    }
}
