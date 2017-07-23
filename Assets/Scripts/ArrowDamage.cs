using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : MonoBehaviour {

        //declare variables
        public float Life;
        public float Damage;
        public float ArrowSpeed;
        public float Knockback;


        // Update is called once per frame
        void Update()
        {
        //move the arrow forward
        transform.position += transform.up * ArrowSpeed * Time.deltaTime;

        //lifetime of the arrow
        Life -= Time.deltaTime;
        if (Life < 0)
            Destroy(this.gameObject);
        }

        //detect if the arrow is hitting an enemy
        void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.tag == "Resource1" || other.gameObject.tag == "Resource2" || other.gameObject.tag == "Resource3")
        {
            Destroy(this.gameObject);
        }
        //makesure that the enemy tagged zombie
        if (other.gameObject.tag == "Zombie")
            {   //run a function the hit object called damage, and give it the arguement Damage
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
            
            Destroy(this.gameObject);

            
        }
        }
   
}
