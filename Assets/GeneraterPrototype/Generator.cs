using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Generator : MonoBehaviour {

    public GameObject[] SpawnedItems;

    public int XMAX;
    public int YMAX;
    public int ItemsToSpawn;

    // Use this for initialization
    void Start () {
        for (int x = 0; x < ItemsToSpawn; x++)
        {

            int rndnum = Random.Range(0, SpawnedItems.Length);
            Instantiate(SpawnedItems[rndnum],new Vector2(Random.Range(this.transform.position.x - (XMAX / 2), this.transform.position.x + (XMAX /2)),Random.Range(transform.position.y - (YMAX / 2), transform.position.y + (YMAX / 2))) , transform.rotation);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
