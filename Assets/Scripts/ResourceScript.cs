using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour {
    public int resourcehealth;
    public int ResourceDrop;

    

    public GameObject ResourceTypeGO;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ResourceCollect()
    {
        resourcehealth -= 1;
        if (resourcehealth < 1)
        {
                for (int i = 0; i < ResourceDrop ; i++)
                {
                    Instantiate(ResourceTypeGO, new Vector2(Random.Range(transform.position.x - 1, transform.position.x + 1), Random.Range(transform.position.y - 1, transform.position.y + 1)), transform.rotation);
                }
            Destroy(this.gameObject);
        }
    }
}
