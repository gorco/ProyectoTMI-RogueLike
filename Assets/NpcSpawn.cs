using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawn : MonoBehaviour {

    [SerializeField]
    GameObject pointOfSpawn;
    GameObject whatSpawnClone;
    [SerializeField]
    GameObject prefab;

    // Use this for initialization
    void Start () {
		
	}

    public void spawn(int n)//numero de nivel
    {
        if (n > 0)
        {
            whatSpawnClone.GetComponent<ThrowDialog>().fieldName = "npc" + (int)GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentLevel + 1;
            whatSpawnClone = Instantiate(prefab, pointOfSpawn.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            whatSpawnClone.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
