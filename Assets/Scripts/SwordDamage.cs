using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour {
    public float Damage;
    public float Knockback;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            other.SendMessageUpwards("Damage", Damage);

            //this does the knockback
            //save others rotation
            Quaternion old = other.transform.rotation;
            //make other object look at us
            other.transform.LookAt(this.transform);
            //make other object look 180degrees away from us
            other.transform.Rotate(0, 0, 180);
            //move a distance
            other.transform.position += transform.up * Knockback * Time.deltaTime;
            //reset the rotation to original
            other.transform.rotation = old;

        }
    }
}
