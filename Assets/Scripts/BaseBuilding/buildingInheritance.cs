using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingInheritance : MonoBehaviour {

    public float BuildingHealth;
    public float BuildingMaxHealth;
    protected float percentHealth;

    public Sprite[] Image;

    //fire variables
    public bool canBeSetOnFire = false;
    public bool OnFire;
    public float fireTimer;
    public float firedamageCD;
    public float firedamage;
    public float firespreadchancePerFireDamageCD;
    public bool SpreadFireBool;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnFireFunction();
    }

    public void BuildingTakeDamage(float Damage)
    {
        BuildingHealth -= Damage;
        if (BuildingHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        percentHealth = BuildingHealth / BuildingMaxHealth * 100;
        
        if (percentHealth >= 50 && percentHealth <70)
        {
            GetComponent<SpriteRenderer>().sprite = Image[1];
        }
        else if (percentHealth >= 25 && percentHealth < 50)
        {
            GetComponent<SpriteRenderer>().sprite = Image[2];
        }
        else if (percentHealth >= 0 && percentHealth < 25)
        {
            GetComponent<SpriteRenderer>().sprite = Image[3];
        }
    }
    // repair still needs implementing
    public void RepairBuilding(float RepairAmount)
    {
        BuildingHealth += RepairAmount;
        if (BuildingHealth > BuildingMaxHealth)
        {
            BuildingHealth = BuildingMaxHealth;
        }
    }

    public void onfire()
    {
        OnFire = true;
    }

    public void OnFireFunction()
    {
        
        
        {
            if (OnFire == true)
            {
                
                
                    if (!this.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        this.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    
                    fireTimer -= Time.deltaTime;

                    if (fireTimer < 0)
                    {
                        //do damage to this zombie
                        BuildingTakeDamage(firedamage);

                        //spread fire
                        float rnd = Random.Range(0, 100);
                        if (rnd < firespreadchancePerFireDamageCD)
                        {
                            SpreadFireBool = true;
                        }

                        //fire cooldown
                        fireTimer = firedamageCD;
                    }
                
            }
        }
    }
}
