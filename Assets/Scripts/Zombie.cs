using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    //declare varialbes

    public GameObject bloodSplatter;

    public float Health;
    public int EXPReward;
    public float zombieDamage;
    private float timer;
    public float attackcd;
    private float timerSound;
    private float rndsound;
    public float growlchancepersecond;
    public float growlCD;
    public float buildingsoundhitfreq;
    public bool OnFire;
    public float fireTimer;
    public float firedamageCD;
    public float firedamage;
    public float firespreadchancePerFireDamageCD;
    public bool SpreadFireBool;

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        //zombie sound
        timerSound -= Time.deltaTime;
        if (timerSound < 0)
        {
            rndsound = Random.Range(0, 300);
            if (rndsound <= growlchancepersecond)
            {
                SoundManageScript.instance.playZombieGrowl(this.transform);
            }
            timerSound = growlCD;
        }

        OnFireFunction();

    }

    void Damage(float _Damage)
    {   //reduce damage and kill if required
        Health -= _Damage;

        //temp set on fire when take damage ///////////////////////////////////////////////////////
        OnFire = true;

        Vector3 angle = transform.rotation.eulerAngles;

        Quaternion newAngle = Quaternion.Euler(angle.x, angle.y, angle.z + 90);

        GameObject newBloodSplatter = Instantiate(bloodSplatter, transform.position, newAngle) as GameObject;

        if (Health <= 0)
        {

            this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);

            this.gameObject.GetComponent<Unit>().zombiedead = true;

            //drop loot chance
            DropSpawnScript.instance.Drop(this.transform);
            //remove the zombie
            Destroy(this.gameObject);

            //reward the player with EXP
            Player.instance.Exp += EXPReward;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (timer < 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.instance.Health -= zombieDamage * Time.deltaTime;
                timer = attackcd;
            }
            if (collision.gameObject.tag == "Building")
            {
                collision.gameObject.SendMessage("BuildingTakeDamage", 0.2f);
                int rnd = Random.Range(0, 100);
                if (rnd < buildingsoundhitfreq)
                {
                    SoundManageScript.instance.playBuildingAttacked(this.transform);
                }

            }
            if (OnFire && SpreadFireBool)
            {
                if (collision.gameObject.tag == "Zombie")
                {
                    collision.gameObject.SendMessage("setonfire");
                    
                }
                if (collision.gameObject.tag == "Building")
                {
                    collision.gameObject.SendMessage("onfire");
                }

                SpreadFireBool = false;

            }
                
        }
    }

    public void setonfire()
    {
        OnFire = true;
    }

    public void OnFireFunction()
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
                Damage(firedamage);

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
