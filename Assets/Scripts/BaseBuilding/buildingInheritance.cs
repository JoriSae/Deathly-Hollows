using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingInheritance : MonoBehaviour {

    public float BuildingHealth;
    public float BuildingMaxHealth;
    protected float percentHealth;

    public Sprite[] Image;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
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

    public void RepairBuilding(float RepairAmount)
    {
        BuildingHealth += RepairAmount;
        if (BuildingHealth > BuildingMaxHealth)
        {
            BuildingHealth = BuildingMaxHealth;
        }
    }
}
