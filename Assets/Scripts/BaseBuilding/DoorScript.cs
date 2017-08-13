using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public float BuildingHealth;
    public float BuildingMaxHealth;
    public float timer;
    public float doorclosetime;
    private bool dooropen = false;
    public Sprite[] ImageDoor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (dooropen == true)
        {
            timer += Time.deltaTime;
            if (timer > doorclosetime)
            {
                this.GetComponent<Collider2D>().isTrigger = false;
                GetComponent<SpriteRenderer>().sprite = ImageDoor[0];
                dooropen = false;
            }
        }
	}
    public void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
            GetComponent<SpriteRenderer>().sprite = ImageDoor[1];
            this.GetComponent<Collider2D>().isTrigger = true;
            timer = 0;
            dooropen = true;
            }
    }
    public void BuildingTakeDamage(float Damage)
    {
        BuildingHealth -= Damage;
        if (BuildingHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
