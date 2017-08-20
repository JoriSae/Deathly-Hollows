using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : buildingInheritance {

    // tower variables
    public float TowerRange;
    public float TowerDamage;
    public float TowerShootCD;
    private float TowerShootTimer;
    public float TowerArrowSpeed;

    private GameObject Target;

    public GameObject ArrowGO;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TowerShootTimer -= Time.deltaTime;
        if (TowerShootTimer < 0)
        {
            ShootTower();
            TowerShootTimer = TowerShootCD;
        }

        //run on fire function
        OnFireFunction();

        if (Target != null)
        {
            this.transform.GetChild(1).gameObject.transform.LookAt(Target.transform);
        }
	}

    public void ShootTower()
    {   // aquire target
        
        if (Target == null)
        {
            //check all targets
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, TowerRange);

            //if tower is in range
            if (colliders.Length > 0)
            {
                // loop over all the colliders
                foreach (Collider2D New2d in colliders)
                {
                    // check of which objects are zombies
                    if (New2d.gameObject.CompareTag ("Zombie"))
                    {
                        //set new target
                        Target = New2d.gameObject;
                    }
                }
            }
        }
        else
        {
            // if target aquired aim the arrow at the zombie
            GameObject iarrow = Instantiate(ArrowGO, this.transform.position, this.transform.GetChild(1).gameObject.transform.rotation);
            iarrow.transform.localRotation = this.transform.GetChild(1).gameObject.transform.rotation;
            iarrow.transform.Rotate(transform.up * 90);
            iarrow.transform.Rotate(transform.forward * 90);
            iarrow.transform.position -= new Vector3(0,0,4);

            iarrow.GetComponent<ArrowDamage>().Damage = TowerDamage;
            iarrow.GetComponent<ArrowDamage>().ArrowSpeed = TowerArrowSpeed;

        }
    }
    

    
}

