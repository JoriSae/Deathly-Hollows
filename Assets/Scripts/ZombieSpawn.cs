using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public static ZombieSpawn zombieSpawn;


    public Transform Zombie;
    public Transform randomLocation;
    public float minDistance = 4;
    public float spawnRange = 20;
    public int spawnAmount = 7;
    public float SpawnInterval = 3;

    float spawnInterval = 2;
    Collider myCollider;
    Vector3 pos;

    // Use this for initialization
    void Start()
    {
        zombieSpawn = this;
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0)
        {
            spawnInterval = SpawnInterval;

            for (int i = 0; i < spawnAmount; i++)
            {
                randomLocation.position = new Vector2(Random.Range(this.gameObject.transform.position.x - spawnRange, this.gameObject.transform.position.x + spawnRange), (Random.Range(this.gameObject.transform.position.y - spawnRange, this.gameObject.transform.position.y + spawnRange)));

                Instantiate(Zombie.gameObject, new Vector2(randomLocation.position.x, randomLocation.position.y), Quaternion.identity);
            }
        }
    }
}