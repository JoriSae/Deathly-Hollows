using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    public float Health;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Damage(float _Damage)
    {
        Health -= _Damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Zombie Killed");
        }
    }
}
